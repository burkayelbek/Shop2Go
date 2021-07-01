using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop2GoAPI.Models;
using Shop2GoAPI.Models.Interfaces;

namespace Shop2GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public ProductsController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Products>> GetProducts()
        {
            var products = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved
            }).ToList();

            if (products.Count == 0)
            {
                return BadRequest(new { message = "Products could not be found!" });
            }
            else
            {
                return Ok(products);
            }
        }

        // GET: api/Products/GetActiveProducts
        [HttpGet("GetActiveProducts")]
        public async Task<ActionResult> GetActiveProducts() 
        {
            var products = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,  // isApproved = 1 (Accepted)
                User = new
                {
                    UserName = x.User.UserName
                },
                isSold = x.isSold
            }).Where(x => x.isApproved == 1 && x.isSold == 0).OrderByDescending(x => x.PublishedDate).ToList();

            List<object> result = new List<object>();
            foreach (var product in products)
            {
                var tempImage = _context.ProductImage.Where( x=> x.ProductId == product.Id)
                    .Select(x => new {ProductId = x.ProductId , Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                var userProfilePicture = _context.User.Where(x => x.Id == product.UserID)
                                 .Select(x => new
                                 {
                                     x.ProfilePicture
                                 })
                                 .FirstOrDefault();

                var profilePicResult = new 
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : "" 
                };

                result.Add(new { tempImage, product, profilePicResult });
            }

            if (products.Count == 0)
            {
                return BadRequest(new { message = "Products could not be found!" });
            }
            return Ok(result);
            
        }
        // GET: api/Admin/GetPassiveProducts
        [HttpGet("GetPassiveProducts")]
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult> GetPassiveProducts() 
        {
            var products = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,      // isApproved = 0 (Waiting) || isApproved = 2 (Rejected) 
                RejectedMessage = x.RejectedMessage,
                User = new
                {
                    UserName = x.User.UserName
                }

            }).Where(x => x.isApproved == 0 || x.isApproved == 2).OrderByDescending(x => x.PublishedDate).ToList();

            List<object> result = new List<object>();
            foreach (var product in products)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                var userProfilePicture = _context.User.Where(x => x.Id == product.UserID)
                                .Select(x => new
                                {
                                    x.ProfilePicture
                                })
                                .FirstOrDefault();

                var profilePicResult = new
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
                };

                result.Add(new { tempImage, product, profilePicResult });
            }

            if (products.Count == 0)
            {
                return BadRequest(new { message = "Products could not be found!" });
            }
            return Ok(result);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<Products>> PostProducts(IProductAddModel _products)
        {
            string currentUserId = User.Claims.First(x => x.Type == "UserID").Value;
            var categoryId = _context.Category.Where(x => x.Id == _products.CategoryId).FirstOrDefault();

            if(String.IsNullOrEmpty(_products.Title) || _products.Price < 0)
            {
                return BadRequest(new { message = "Price cannot be lower than zero or title must be filled!" });
            }
            if (_products.Image.Count > 6)
            {
                return BadRequest(new { message = "You cannot add more than 6 images!" });
            }

            if (categoryId == null)
            {
                return BadRequest(new { message = "Category must be selected!" });
             
            }

            Products newProduct = new Products()
            {
                Title = _products.Title,
                Price = _products.Price,
                Description = _products.Description,
                PublishedDate = DateTime.Now,
                CategoryId = _products.CategoryId,
                UserID = currentUserId,
                isApproved = 0
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            List<ProductImage> arr = new List<ProductImage>();


            foreach (var item in _products.Image)
            {
                try
                {
                    ProductImage tempImage = new ProductImage();

                    System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(item);
                    System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
                    System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(750, 422, null, IntPtr.Zero);
                    System.IO.MemoryStream myResult = new System.IO.MemoryStream();
                    newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tempImage.Image = myResult.ToArray();
                    tempImage.ProductId = newProduct.Id;
                    arr.Add(tempImage);
                }
                catch (Exception e)
                {
                    _context.Remove(newProduct);
                    await _context.SaveChangesAsync();
                    return BadRequest(new { message = "There is something wrong with the image!"});
                    
                }
            }
            _context.ProductImage.AddRange(arr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = newProduct.Id }, newProduct);

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;

            var products = _context.Products.Where(x => x.UserID == userId && x.Id == id).FirstOrDefault();

            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Object> GetProductById(int id)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var products = _context.Products.Select(x => new 
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,  // isApproved = 1 (Accepted)
                User = new
                {
                    UserName = x.User.UserName
                },
            }).Where(x=> x.Id == id).FirstOrDefault();

            var rndProducts = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,   // isApproved = 1 (Accepted)
                User = new
                {
                    UserName = x.User.UserName
                },
                isSold = x.isSold

            }).Where(x => x.UserID != USERID && x.isSold == 0 && x.isApproved == 1 && x.Category.Id == products.Category.Id && x.Id != products.Id).OrderBy(x => Guid.NewGuid()).Take(6).ToList();

            var image = await _context.ProductImage.Where(x => x.ProductId == id).Select(x => new { Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) }).ToListAsync();

            List<object> randomProductList = new List<object>();
            foreach (var randomProduct in rndProducts)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == randomProduct.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                var userProfilePicture = _context.User.Where(x => x.Id == randomProduct.UserID)
                                .Select(x => new
                                {
                                    x.ProfilePicture
                                })
                                .FirstOrDefault();

                var profilePicResult = new
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
                };

                randomProductList.Add(new { tempImage, randomProduct, profilePicResult });
            }
            var result = new { products, image, randomProductList };
            if(products == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Products/GetActiveProductByUser
        [HttpGet("GetActiveProductByUser")]
        [Authorize(Roles ="User")]
        public async Task<Object> GetActiveProductByUser()
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;

            var products = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,
                isSold = x.isSold,
                UserName = x.BuyerUserName

            }).Where(x => x.UserID == userId && x.isApproved == 1 && x.isSold != 1).OrderByDescending(x=>x.PublishedDate).ToList(); // Get Active products

            List<object> result = new List<object>();
            foreach (var product in products)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                result.Add(new { tempImage, product });
            }
            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Products/GetPassiveProductByUser
        [HttpGet("GetPassiveProductByUser")]
        [Authorize(Roles="User")]
        public async Task<Object> GetPassiveProducyByUser()
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var products = _context.Products.Select(x => new 
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,
                RejectedMessage = x.RejectedMessage,
                isSold = x.isSold

            }).Where(x => x.UserID == USERID && (x.isApproved == 0 || x.isApproved == 2) && x.isSold != 1).OrderBy(x => x.isApproved).ToList();

            List<object> result = new List<object>();
            foreach (var product in products)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                result.Add(new { tempImage, product });
            }

            if (products.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/User/GetSoldProductsByUserId
        [HttpGet("GetSoldProductsByUser")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetSoldProductsByUser()
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var products = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,
                RejectedMessage = x.RejectedMessage,
                User = new
                {
                    UserName = x.User.UserName
                },
                isSold = x.isSold,
                BuyerName = x.BuyerUserName

            }).Where(x => x.UserID == USERID && x.isSold == 1 && x.isApproved == 1).OrderByDescending(x => x.PublishedDate).ToList();

            List<object> result = new List<object>();
            foreach (var product in products)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                result.Add(new { tempImage, product });
            }

            if (products.Count == 0)
            {
                return BadRequest(new { message = "Products could not be found!" });
            }
            return Ok(result);
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public async Task<ActionResult> UpdateProduct(IProductPatchModel model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var product = _context.Products.Where(x => x.UserID == USERID && x.Id == model.Id).FirstOrDefault();

            if (product == null)
            {
                return BadRequest(new { message = "Product could not found!"});
            }

            product.Title = model.Title;
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.isApproved = 0;

            var result = _context.Products.Update(product);
            await _context.SaveChangesAsync();

            List<ProductImage> arr = new List<ProductImage>();

            foreach (var item in model.Image)
            {
                try
                {
                    ProductImage tempImage = new ProductImage();

                    System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(item);
                    System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
                    System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(750, 422, null, IntPtr.Zero);
                    System.IO.MemoryStream myResult = new System.IO.MemoryStream();
                    newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tempImage.Image = myResult.ToArray();
                    tempImage.ProductId = product.Id;
                    arr.Add(tempImage);
                }
                catch (Exception e)
                {
                    _context.Remove(product);
                    await _context.SaveChangesAsync();
                    return BadRequest(new { message = "There is something wrong with the image!" });

                }

            }
            _context.ProductImage.AddRange(arr);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        // GET: api/Products/{id}
        [HttpGet("GetActiveProductByUserId/{id}")]
        [Authorize]
        public async Task<Object> GetActiveProductByUserId(string id)
        {
            var allProducts = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,
                isSold = x.isSold
            }).Where(x => x.UserID == id && x.isApproved == 1 && x.isSold == 0).OrderByDescending(x=> x.PublishedDate).ToList();


            List<object> productList = new List<object>();
            foreach (var tempProduct in allProducts)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == tempProduct.Id)
                    .Select(x => new { Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();


                productList.Add(new { tempProduct,tempImage });
            }


            if (allProducts == null)
            {
                return NotFound();
            }
            return Ok(productList);
        }

        [HttpPost("ChangeProductStatusToSold")]
        [Authorize]
        public async Task<ActionResult> ChangeProductStatusToSold(IUpdateProductSoldStatus model)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var buyer = _context.User.Where(x => x.UserName == model.UserName).FirstOrDefault();

            var product = _context.Products.Where(x => x.UserID == USERID && x.Id == model.Id).FirstOrDefault();

            if (product == null && buyer == null)
            {
                return BadRequest(new { message = "User could not found!" });
            }
            if (buyer.Id == USERID)
            {
                return BadRequest(new { message = "You cannot add yourself!" });
            }

            product.isSold = 1;
            product.BuyerUserName = buyer.UserName;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Products/GetPassiveProductByUser
        [HttpGet("GetAllRatingsByProductId/{id}")]
        [Authorize]
        public async Task<Object> GetAllRatingsByProductId(int id)
        {
            var product = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,

            }).Where(x => x.Id == id).FirstOrDefault();

            double avgRating = 0;
            var reviews = _context.ReviewRating.Select(x => new
            {
                RecieverId = x.RecieverId,
                SenderId = x.SenderId,
                StarRating = x.StarRating,
            

            }).Where(x => x.SenderId != product.UserID && x.RecieverId == product.UserID).ToList();

            foreach (var review in reviews)
            {
                avgRating += review.StarRating;
            }

            avgRating /= reviews.Count();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new { AverageRating = avgRating , Product = product });
            
        }

        [HttpGet("GetProductsByCategoryName/{name}")]
        [Authorize]
        public async Task<ActionResult> GetProductsByCategoryName(string name)
        {
            var category = _context.Category.Where(x => x.Name == name).FirstOrDefault();
            if (category == null)
            {
                return BadRequest(new { message = "Category could not found!" });

            }

            var allProducts = _context.Products.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Description = x.Description,
                PublishedDate = x.PublishedDate,
                CategoryId = x.CategoryId,
                Category = new Category
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                UserID = x.UserID,
                isApproved = x.isApproved,  // isApproved = 1 (Accepted)
                User = new
                {
                    UserName = x.User.UserName
                },
                isSold = x.isSold
            }).Where(x => x.isApproved == 1 && x.isSold == 0 && x.CategoryId == category.Id).OrderByDescending(x => x.PublishedDate).ToList();

            List<object> productList = new List<object>();
            foreach (var product in allProducts)
            {
                var tempImage = _context.ProductImage.Where(x => x.ProductId == product.Id)
                    .Select(x => new { ProductId = x.ProductId, Image = "data:image/jpg;base64," + Convert.ToBase64String(x.Image) })
                    .FirstOrDefault();

                var userProfilePicture = _context.User.Where(x => x.Id == product.UserID)
                                 .Select(x => new
                                 {
                                     x.ProfilePicture
                                 })
                                 .FirstOrDefault();

                var profilePicResult = new
                {
                    ProfilePicture = userProfilePicture.ProfilePicture != null ? "data:image/jpg;base64," + Convert.ToBase64String(userProfilePicture.ProfilePicture) : ""
                };

                productList.Add(new { tempImage, product, profilePicResult });
            }

            if (category == null || productList.Count <0)
            {
                return BadRequest(new { message = "Products could not found!" });

            }
            return Ok(productList);

        }

    }
}
