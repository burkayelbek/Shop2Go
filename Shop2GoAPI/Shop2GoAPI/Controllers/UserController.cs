using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop2GoAPI.Models;
using Shop2GoAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop2GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ApplicationContext _context;
        private readonly ApplicationSettings _appSettings;
        private readonly IMailer _mailer;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context, IOptions<ApplicationSettings> appSettings, IMailer mailer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _appSettings = appSettings.Value;
            _mailer = mailer;
        }


        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var userList = _context.User.Select(x=> new 
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName,
                UserName = x.UserName,
                Email = x.Email,
            }).Where(x => x.Id != USERID).ToList();

            List<object> userInformationList = new List<object>();
            foreach (var user in userList)
            {
                var restrictedUserInformation = _context.RestrictedUsers.Select(x => new { UserID = x.UserID, RestrictedDate = x.RestrictedDate }).Where(x=> x.UserID == user.Id).FirstOrDefault();
                var userProfilePicture = _context.User.Where(x => x.Id == user.Id)
                               .Select(x => new
                               {
                                   x.ProfilePicture
                               })
                               .FirstOrDefault();

                var ppResult = new
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
                };

                userInformationList.Add(new { restrictedUserInformation, user, ppResult });
            }

            if (userList == null)
            {
                return BadRequest(new { message = "User list could not fetched!" });
            }

            return Ok(userInformationList);
        }

        // POST: api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<Object> Register(IUser _userRegisterModel)
        {
            _userRegisterModel.Role = "User";
            var emailControl = _context.User.FirstOrDefault(x => x.Email == _userRegisterModel.Email);
            var user = _context.User.Where(x => x.UserName == _userRegisterModel.UserName).FirstOrDefault();

            if (emailControl != null)
            {
                return BadRequest(new { message = "Email is already taken!" });
            }

            if (String.IsNullOrWhiteSpace(_userRegisterModel.FirstName) || String.IsNullOrWhiteSpace(_userRegisterModel.LastName) || String.IsNullOrWhiteSpace(_userRegisterModel.UserName) || String.IsNullOrWhiteSpace(_userRegisterModel.Email))
            {
                return BadRequest(new { message = "Do not put empty spaces!" });
            }

            if (String.IsNullOrEmpty(_userRegisterModel.FirstName) || String.IsNullOrEmpty(_userRegisterModel.LastName) || String.IsNullOrEmpty(_userRegisterModel.UserName) || String.IsNullOrEmpty(_userRegisterModel.Email))
            {
                return BadRequest(new { message = "All required informations must be filled!" });
            }

            if (_userRegisterModel.FirstName.EndsWith(" ") || _userRegisterModel.FirstName.StartsWith(" ") ||
                _userRegisterModel.LastName.EndsWith(" ") || _userRegisterModel.LastName.StartsWith(" ") ||
                _userRegisterModel.UserName.EndsWith(" ") || _userRegisterModel.UserName.StartsWith(" ") ||
                _userRegisterModel.Email.EndsWith(" ") || _userRegisterModel.Email.StartsWith(" ") ||
                _userRegisterModel.FirstName.Contains(" ") || _userRegisterModel.LastName.Contains(" ") ||
                _userRegisterModel.UserName.Contains(" ") || _userRegisterModel.Email.Contains(" ")) 
            {
                return BadRequest(new { message = "Do not put empty spaces!"});
            }
            if (user != null)
            {
                return BadRequest(new { message = "This username is already taken!" });
            }

            var applicationUser = new User()
            {
                UserName = _userRegisterModel.UserName.Trim(),
                FirstName = _userRegisterModel.FirstName.Trim(),
                LastName = _userRegisterModel.LastName.Trim(),
                Email = _userRegisterModel.Email.Trim()
            };
          
            try
            {
                if ((_userRegisterModel.Password == _userRegisterModel.ConfirmPassword) && (_userRegisterModel.ConfirmPassword.Length >= 6 && _userRegisterModel.ConfirmPassword.Length >= 6))
                {
                    var result = await _userManager.CreateAsync(applicationUser, _userRegisterModel.Password);
                    await _userManager.AddToRoleAsync(applicationUser, _userRegisterModel.Role);
                    return Ok(result);

                }
                else
                {
                    if ((_userRegisterModel.ConfirmPassword.Length < 6) && (_userRegisterModel.ConfirmPassword.Length < 6))
                    {
                        return BadRequest(new { message = "Passwords length must be greater than 6!" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Passwords must be same!" });
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        // POST: api/User/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(ILogin _userLoginModel)
        {
            var user = await _userManager.FindByNameAsync(_userLoginModel.UserName) ?? await _userManager.FindByEmailAsync(_userLoginModel.UserName);
            var userPassword = await _userManager.CheckPasswordAsync(user,_userLoginModel.Password);
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == user.Id).FirstOrDefault();

            if ((user == null) && userPassword)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });

            }
            if (restrictedUser != null)
            {
                return BadRequest(new { message = "Account is restricted by admin!" });
            }
            var role = await _userManager.GetRolesAsync(user);
            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new { token });
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<Object> GetUser()
        {

            string userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return BadRequest(new { message = "This operation cannot be allowed!"});

            }

            return new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.UserName,
                user.Email
            };

        }
      
        [HttpPost("ToggleFavorite")]
        [Authorize(Roles ="User")]
        public async Task<Object> ToggleFavorite(IAddFavorites model)
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;
            var productId =  _context.Favorites.Where(x => x.ProductId == model.ProductId).FirstOrDefault();

            var productsOwner = _context.Products.Where(x => x.UserID == userId && x.Id == model.ProductId).FirstOrDefault();
            var productsNotOwner = _context.Products.Where(x => x.UserID != userId && x.Id == model.ProductId).FirstOrDefault();

            if (productId != null)
            {
                if (productsNotOwner == null)
                {
                    return BadRequest(new { message = "Cannot remove anyone else's product in favorite list!" });
                }
                else
                {
                    _context.Favorites.Remove(productId);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Product Successfully Removed!" });
                }
               
            }
            else
            {
                try
                {
                    if (productsOwner != null)
                    {
                        return BadRequest(new { message = "Cannot add own product!" });
                    }
                    else
                    {
                        Favorites addFavorites = new Favorites()
                        {
                            UserID = userId,
                            ProductId = model.ProductId,

                        };
                        _context.Favorites.AddAsync(addFavorites);
                        await _context.SaveChangesAsync();
                        return Ok(new { message = "Product Successfully Added!"});
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            
        }


        [HttpGet("GetFavoritedProducts")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult> GetFavoritedProducts()
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var favoritedProducts = _context.Favorites.Where(x => x.UserID == USERID).Select(x => new
            {
                ProductId = x.ProductId,
                UserID = USERID,
                Products = new
                {
                    Id = x.Products.Id,
                    Title = x.Products.Title,
                    Description = x.Products.Description,
                    PublishedDate = x.Products.PublishedDate,
                    Price = x.Products.Price,
                    Category = new Category
                    {
                        Id = x.Products.Category.Id,
                        Name = x.Products.Category.Name
                    },
                    User = new
                    {
                        UserName = x.Products.User.UserName
                    }
                }
            }).ToList();

            List<object> result = new List<object>();
            foreach (var product in favoritedProducts)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.ProductId)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                result.Add(new { favoritedProducts, tempImage });
            }


            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetFavoritedProductByProductId/{id}")]
        [Authorize]
        public async Task<ActionResult> GetFavoritedProductByProductId(int id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var favoritedProducts = _context.Favorites.Where(x => x.ProductId == id && x.UserID == USERID).Select(x => new
            {
                ProductId = x.ProductId,
                UserID = USERID,
                Products = new
                {
                    Id = x.Products.Id,
                    Title = x.Products.Title,
                    Description = x.Products.Description,
                    PublishedDate = x.Products.PublishedDate,
                    Price = x.Products.Price,
                    Category = new Category
                    {
                        Id = x.Products.Category.Id,
                        Name = x.Products.Category.Name
                    },
                    User = new
                    {
                        UserName = x.Products.User.UserName
                    }
                }
            }).FirstOrDefault();

            return Ok(favoritedProducts);
        }

        [HttpPatch("UpdateUser")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(IUpdateUser model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var currentUser = await _userManager.FindByIdAsync(USERID);
            var userNameControl = _context.User.FirstOrDefault(x => x.UserName == model.UserName);
            var emailControl = _context.User.FirstOrDefault(x => x.Email == model.Email);

            if (currentUser == null)
            {
                return BadRequest(new { message = "An error occurred while updating!" });
            }
            
            else
            {
                if (currentUser.UserName == model.UserName)
                {
                    if (emailControl != null)
                    {
                        if (currentUser.Email != model.Email)
                        {
                            return BadRequest(new { message = "Email is already taken!" });
                        }

                    }
                    currentUser.UserName = currentUser.UserName;
                    currentUser.FirstName = model.FirstName;
                    currentUser.LastName = model.LastName;
                    currentUser.Email = model.Email;
                    var result = await _userManager.UpdateAsync(currentUser);

                    return Ok(result);
                }
                else if (currentUser.Email == model.Email)
                {
                    if (userNameControl != null)
                    {
                        return BadRequest(new { message = "Username is already taken!" });
                    }
                    currentUser.UserName = model.UserName;
                    currentUser.FirstName = model.FirstName;
                    currentUser.LastName = model.LastName;
                    currentUser.Email = currentUser.Email;
                    var result = await _userManager.UpdateAsync(currentUser);

                    return Ok(result);
                }
                else if (emailControl.Email == model.Email && userNameControl.UserName == model.UserName)
                {
                    return BadRequest(new { message = "Username or email have already taken!" });
                }
                else
                {
                  
                        
                    currentUser.UserName = model.UserName;
                    currentUser.FirstName = model.FirstName;
                    currentUser.LastName = model.LastName;
                    currentUser.Email = model.Email;

                    var result = await _userManager.UpdateAsync(currentUser);

                    return Ok(result);
                        
                        
                   
                }

            }

        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(IChangePasswordModel model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var currentUser = await _userManager.FindByIdAsync(USERID);

             if(currentUser == null)
            {
                return BadRequest(new { message = "An error occurred while updating!" });
            }
            else
            {
                try
                {
                    if (await _userManager.CheckPasswordAsync(currentUser, model.CurrentPassword))
                    {
                        if((model.Password == model.ConfirmPassword) && (model.ConfirmPassword.Length >= 6 && model.ConfirmPassword.Length >= 6))
                        {
                            var result = await _userManager.ChangePasswordAsync(currentUser, model.CurrentPassword, model.Password);

                            await _signInManager.RefreshSignInAsync(currentUser);

                            return Ok(result);
                        }
                        else
                        {
                            if ((model.ConfirmPassword.Length < 6) && (model.ConfirmPassword.Length < 6))
                            {
                                return BadRequest(new { message = "Passwords length must be greater than 6!" });
                            }
                            else
                            {
                                return BadRequest(new { message = "Passwords must be same!" });
                            }
                        }
                        
                    }
                    else
                    {
                        return BadRequest(new { message = "Current Password does not match!" });
                    }
                    
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(IForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            try
            {
                if (user != null)
                {
                    string message;
                    var passwordResetLink = $"http://localhost:{4200}/ResetPassword?Email={model.Email}&UserID={user.Id}";
                    message = $@"<p><a href=""{passwordResetLink}"">Click here to reset password!</a></p>";


                    var mailResult = _mailer.SendEmailAsync(model.Email, "Reset Password", "<h1>Password Reset Link</h1>" + "<hr>" + "<h3>" + message + "</h3>");
                    return Ok(new {message = "Password reset link sent!" });

                }

                else
                {
                    return BadRequest(new { message = "User email could not found!" });
                }

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }

        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(IResetPasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);
                
            if(user != null && (model.Email == user.Email))
            {
                if (model.Password.Length >= 6 )
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var changedPassword = await _userManager.ResetPasswordAsync(user, token, model.Password);

                    if (changedPassword.Succeeded)
                    {
                        _context.SaveChanges();
                        return Ok(new { message = "Password changed successfully!"});
                    }
                    else
                    {
                        return BadRequest(new { message = "Password could not changed!" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Password length does not enough!" });
                }
            }
            else
            {
                return BadRequest(new { message = "User could not found!" });
            }
           
        }

        [HttpPost]
        [Route("SendUserTicket")]
        [Authorize]
        public async Task<Object> SendUserTicket(IUserTicketModel model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(USERID);

            if (String.IsNullOrEmpty(model.Message) && (model.Priority<0 && model.Priority>2))
            {
                return BadRequest(new { message = "Incorrect Post Operation!"});
            }
            try
            {
                string guid = "";
                guid = Guid.NewGuid().ToString("N");

                Ticket tickets = new Ticket()
                {
                    Id = guid,
                    UserID = USERID,
                    UserFullName = user.FirstName + " " + user.LastName,
                    Title = model.Title,
                    Message = model.Message,
                    Priority = model.Priority,
                    Status = 0,
                    CreatedOn = DateTime.Now,
                    FixedDate = null
                };

                _context.Ticket.Add(tickets);
                await _context.SaveChangesAsync();

                return Ok(new {message = "Ticket successfully sent!"});
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpGet("GetTicketByUser")]
        [Authorize(Roles ="User")]
        public async Task<Object> GetTicketByUser()
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            User user = _context.User.FirstOrDefault(x => x.Id == USERID);

            if (user == null || USERID == null)
            {
                return BadRequest(new { message = "User could be found!" });
            }
            else
            {
                try
                {
                    var tickets = _context.Ticket.Select(x => new
                    {
                        Id = x.Id,
                        UserID = x.UserID,
                        UserFullName = x.UserFullName,
                        Title = x.Title,
                        Message = x.Message,
                        Priority = x.Priority,
                        Status = x.Status,
                        CreatedOn = x.CreatedOn,
                        FixedDate = x.FixedDate,
                        FeedbackMessage = x.FeedbackMessage
                    }).Where(x => x.UserID == USERID).OrderByDescending(x => x.CreatedOn).ToList();

                    return tickets;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        [HttpGet("GetTicketByUserId/{id}")]
        [Authorize(Roles = "User")]
        public async Task<Object> GetTicketByUserId(string id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            User user = _context.User.Where(x => x.Id == USERID).FirstOrDefault();

            if (user == null || USERID == null)
            {
                return BadRequest(new { message = "User could be found!" });
            }
            else
            {
                try
                {
                    var tickets = _context.Ticket.Select(x => new
                    {
                        Id = x.Id,
                        UserID = x.UserID,
                        UserFullName = x.UserFullName,
                        Title = x.Title,
                        Message = x.Message,
                        Priority = x.Priority,
                        Status = x.Status,
                        CreatedOn = x.CreatedOn,
                        FixedDate = x.FixedDate,
                    }).Where(x => x.UserID == id).ToList();

                    return tickets;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        [HttpDelete("DeleteTicket/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Ticket>> DeleteTicket(string id)
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;

            var ticket = _context.Ticket.Where(x => x.UserID == userId && x.Id == id).FirstOrDefault();

            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        [HttpPost("DeleteSelectedTicket")]
        [Authorize]
        public async Task<ActionResult> DeleteSelectedTicket(DeleteTicketModel model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var ticketId = _context.Ticket.Where(x => x.UserID == USERID && model.Id.Contains(x.Id)).ToList();

            if (ticketId.Count == 0)
            {
                return BadRequest(new { message = "There are not any ticket find!" });
            }

            _context.Ticket.RemoveRange(ticketId);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPatch("CloseCurrentTicketByUser")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CloseCurrentTicketByUser(IUserCloseTicketModel model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var ticket = _context.Ticket.Where(x=> x.UserID == USERID && x.Id == model.Id).FirstOrDefault();

            if (ticket == null)
            {
                return BadRequest(new { message = "Ticket could not be found!"});
            }
            else
            {
                try
                {
                    ticket.Status = 1;

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    throw new InvalidCastException(ex.Message);
                }
            }
        }
        [HttpPatch("AddProfilePicture")]
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult> AddProfilePicture(IProfilePicture model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(USERID);


            if (model.ProfilePicture != null && user != null)
            {
                System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(model.ProfilePicture);
                System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
                System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(250, 250, null, IntPtr.Zero);
                System.IO.MemoryStream myResult = new System.IO.MemoryStream();
                newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);
                user.ProfilePicture = myResult.ToArray();
            }

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet("GetProfileImage")]
        [Authorize]
        public async Task<ActionResult> GetProfileImage()
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var currentUser = _context.User.Where(x => x.Id == USERID)
                                           .Select(x => new { Id = x.Id, ProfilePicture = "data:image/jpg;base64," + Convert.ToBase64String(x.ProfilePicture) })
                                           .FirstOrDefault();
            if (currentUser == null)
            {
                return BadRequest();
            }
            return Ok(currentUser);
        }

        [HttpGet("GetProfileImageByProductId/{id}")]
        [Authorize]
        public async Task<ActionResult> GetProfileImageByProductId(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            if (product == null)
            {
                return BadRequest();
            }
            var userProfilePicture = _context.User
                               .Select(x => new
                               {
                                   Id = x.Id,
                                   x.ProfilePicture
                               }).Where(x => x.Id == product.UserID).FirstOrDefault();


            var ppResult = new
            {
                ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
            };

            return Ok(ppResult);

        }
        [HttpGet("GetProfileImageByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult> GetProfileImageByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == id).FirstOrDefault();

            if (roles.Contains("Admin") || restrictedUser!=null)
            {
                return BadRequest(new { message = "This user's information could not be loaded!" });
            }
            var userProfilePicture = _context.User.Where(x => x.Id == id)
                                    .Select(x => new { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, UserName = x.UserName, ProfilePicture = "data:image/jpg;base64," + Convert.ToBase64String(x.ProfilePicture) })
                                    .FirstOrDefault();

            return Ok(userProfilePicture);

        }

      
        [HttpGet("GetDetailsByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult> GetDetailsByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == id).FirstOrDefault();

            if (roles.Contains("Admin") || restrictedUser!=null)
            {
                return BadRequest(new { message = "This user's information could not be loaded!" });
            }
            var userInformation = _context.User.Where(x => x.Id == id)
                                    .Select(x => new { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, UserName = x.UserName })
                                    .FirstOrDefault();

            if (userInformation == null)
            {
                return NotFound();
            }
            return Ok(userInformation);
        }


        [HttpPost("SendMessage")]
        [Authorize]
        public async Task<ActionResult> SendMessage(IMessageModel model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var senderUser = _context.User.Where(x => x.Id == USERID).FirstOrDefault();
            var recieverUser = _context.User.Where(x => x.Id == model.RecieverId).FirstOrDefault();

            if ((recieverUser == null) || (senderUser == null))
            {
                return BadRequest(new { message = "Sender or Reciever user could not be found!" });
            }
            if (model.Message.Length >200)
            {
                return BadRequest(new { message = "Message limit exceeded!" });
            }

            var chat = _context.Conversation.Where(x => (x.SenderId == senderUser.Id || x.RecieverId == senderUser.Id) && (x.RecieverId == model.RecieverId || x.SenderId == model.RecieverId) ).FirstOrDefault();

            string chatID = "";
            string guid = "";

            if (chat != null)
            {
                chatID = chat.ConversationId;
            }
            else
            {
                guid = Guid.NewGuid().ToString("N");
                chatID = guid;
            }

            try
            {
                Conversation conv = new Conversation()
                {
                    ConversationId = chatID,
                    SenderId = senderUser.Id,
                    SenderFullName = senderUser.FirstName + " " + senderUser.LastName,
                    RecieverId = recieverUser.Id,
                    RecieverFullName = recieverUser.FirstName + " " + recieverUser.LastName,
                    Message = model.Message,
                    SentTime = DateTime.Now
                };

                await _context.Conversation.AddAsync(conv);
                await _context.SaveChangesAsync();
                return Ok(conv);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("GetMessagesByUser")]
        [Authorize]
        public async Task<Object> GetMessagesByUser()
        {
            string USERID = User.Claims.First(x=> x.Type =="UserID").Value;
            var user = _context.User.FirstOrDefault(x => x.Id == USERID);

            var chatId = _context.Conversation.Where(x => x.SenderId == user.Id || x.RecieverId == user.Id).OrderByDescending(x => x.SentTime).GroupBy(x => x.ConversationId).Select(x => new
            {
                ConversationId = x.Key,
            }).ToList();

            List<object> result = new List<object>();
            foreach (var item in chatId)
            {
                var detailsOfConversation = _context.Conversation.Where(x => x.ConversationId == item.ConversationId).OrderByDescending(x => x.SentTime).FirstOrDefault();

                var tempModel = new
                {
                    RecieverId = detailsOfConversation.RecieverId,
                    participantFullName = detailsOfConversation.RecieverId == user.Id ? detailsOfConversation.SenderFullName : detailsOfConversation.RecieverFullName,
                    conversationId = detailsOfConversation.ConversationId,
                    sentTime = detailsOfConversation.SentTime
                };

                var userProfilePicture = _context.User
                                .Select(x => new
                                {
                                    FullName = x.FirstName + " " + x.LastName,
                                    x.ProfilePicture
                                }).Where(x => x.FullName == tempModel.participantFullName).FirstOrDefault();


                var ppResult = new
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
                };
                result.Add( new { ConversationDetails = tempModel , ppResult });

            }

            return Ok(result);
        }

        [HttpGet("GetMessagesByConversationId/{id}")]
        [Authorize]
        public async Task<Object> GetMessagesByConversationId(string id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var user = _context.Users.FirstOrDefault(x => x.Id == USERID);

            var conversationDetails = _context.Conversation
                .Where(x => (x.ConversationId == id) )
                .OrderBy(x => x.SentTime)
                .Select(x => new
                {
                    RecieverId = x.RecieverId,
                    ConversationId = x.ConversationId,
                    ParticipantFullName = x.RecieverId == user.Id ? x.SenderFullName : x.RecieverFullName,
                    ParticipantId = x.RecieverId == user.Id ? x.SenderId : x.RecieverId,             
                }).FirstOrDefault();

            var history = _context.Conversation
                .Where(x => (x.ConversationId == id))
                .OrderBy(x => x.SentTime)
                .Select(x => new
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    SenderFullName = x.SenderFullName,
                    RecieverId = x.RecieverId,
                    RecieverFullName = x.RecieverFullName,
                    Message = x.Message,
                    SentTime = x.SentTime
                }).ToList();


            return Ok(new { conversationDetails, history});
        }

        [HttpGet("GetMessagesByUserId/{id}")]
        [Authorize]
        public async Task<Object> GetMessagesByUserId(string id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var user = _context.Users.FirstOrDefault(x => x.Id == USERID);
            var convId = _context.Conversation.Where(x => x.SenderId == user.Id || x.RecieverId == user.Id).OrderByDescending(x => x.SentTime).GroupBy(x => x.ConversationId).Select(x => new
            {
                ConversationId = x.Key,
            }).ToList();

            var conversationDetails = _context.Conversation
                .Where(x => (x.RecieverId == id || x.SenderId == id) && (x.SenderId == USERID || x.RecieverId==user.Id))
                .OrderBy(x => x.SentTime)
                .Select(x => new
                {
                    ConversationId = x.ConversationId,
                    ParticipantFullName = x.RecieverId == user.Id ? x.SenderFullName : x.RecieverFullName,
                    ParticipantId = x.RecieverId == user.Id ? x.SenderId : x.RecieverId,
                }).FirstOrDefault();

            var history = _context.Conversation
                .Where(x => (x.RecieverId == id || x.SenderId == id) && (x.SenderId == USERID || x.RecieverId == user.Id))
                .OrderBy(x => x.SentTime)
                .Select(x => new
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    SenderFullName = x.SenderFullName,
                    RecieverId = x.RecieverId,
                    RecieverFullName = x.RecieverFullName,
                    Message = x.Message,
                    SentTime = x.SentTime
                }).ToList();


            return Ok(new { conversationDetails, history });
        }

        [HttpPost("SendRateAndComment")]
        [Authorize]
        public async Task<Object> SendRateAndComment(IReviewAndRatingModel model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var senderUser = _context.User.Select(x => new
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName

            }).Where(x => x.Id == USERID).FirstOrDefault();
            var recieverUser = _context.User.Where(x => x.Id == model.RecieverId).FirstOrDefault();

            if (senderUser ==null && recieverUser == null)
            {
                return BadRequest(new { message = "Sender or Reciever could not found!"});
            }
            if (model.StarRating <= 0)
            {
                return BadRequest(new { message = "You must choose at least 1 star!" });
            }

            //Find every product that the user can leave a review for
            var availableRatings = _context.Products.Where(x => x.UserID == recieverUser.Id && x.isSold == 1 && x.BuyerUserName == senderUser.UserName && x.ReviewId == 0).ToList();
            if(availableRatings.Count <= 0)
            {
                return BadRequest(new { message = "This user does not have the rights to leave review!" });
            }


            ReviewRating reviewRating = new ReviewRating()
            {
                SenderId = USERID,
                SenderFullName = senderUser.FirstName + ' ' + senderUser.LastName,
                RecieverId = model.RecieverId,
                RecieverFullName = recieverUser.FirstName + ' ' + recieverUser.LastName,
                StarRating = model.StarRating,
                Comment = model.Comment,
                SentTime = DateTime.Now

            };

             _context.ReviewRating.Add(reviewRating);
            await _context.SaveChangesAsync();

            //Don't allow multiple reviews back to back in case multiple products were sold to the same person. Mark all as review left.
            foreach (var product in availableRatings)
            {
                product.BuyerUserName = senderUser.UserName;
                product.ReviewId = reviewRating.Id;
            }

            await _context.SaveChangesAsync();

            return Ok(reviewRating);
        }


        [HttpGet("GetAllRatingsByUserId/{id}")]
        [Authorize]
        public async Task<Object> GetAllRatingsByUserId(string id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == USERID);
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == id).FirstOrDefault();

            if (restrictedUser != null)
            {
                return BadRequest(new { message = "This user restricted!" });
            }

            int countReviews = _context.ReviewRating.Where(x => x.RecieverId == id).Count();

            double avgRating = 0;


            var reviews = _context.ReviewRating.Select(x => new
            {
                Id = x.Id,
                SenderId = x.SenderId,
                RecieverId = x.RecieverId,
                SenderFullName = x.SenderFullName,
                StarRating = x.StarRating,
                Comment = x.Comment,
                SentTime = x.SentTime

            }).Where(x => x.SenderId != id && x.RecieverId == id).OrderByDescending(x => x.SentTime).ToList();

            foreach(var review in reviews)
            {
                avgRating += review.StarRating;
            }

            avgRating /= reviews.Count();


            return Ok( new { Reviews = reviews, AverageRating = avgRating , CountReviews = countReviews});
        
        }



        [HttpGet("CanRate/{sellerId}")]
        [Authorize]
        public async Task<ActionResult> CanRate(string sellerId)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;
            var user = _context.Users.Where(x => x.Id == USERID).FirstOrDefault();
            var seller = _context.User.Where(x => x.Id == sellerId).FirstOrDefault();
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == sellerId).FirstOrDefault();

            if (user == null)
            {
                return BadRequest(new { message = "Current user could not be found in database" });
            }

            if (seller == null)
            {
                return BadRequest(new { message = "Seller could not be found in database" });
            }
            if (restrictedUser != null)
            {
                return BadRequest(new { message = "This user restricted in database" });
            }


            var availableRatings = _context.Products.Where(x => x.UserID == seller.Id && x.isSold == 1 && x.BuyerUserName == user.UserName && x.ReviewId == 0).ToList();

            bool canUserRate = false;

            if(availableRatings.Count > 0)
            {
                canUserRate = true;
            }

            return Ok(canUserRate);

        }

    }
}
