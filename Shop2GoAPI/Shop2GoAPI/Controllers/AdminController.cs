using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shop2GoAPI.Models;
using Shop2GoAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ApplicationContext _context;
        private readonly ApplicationSettings _appSettings;
        private readonly IMailer _mailer;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context, IOptions<ApplicationSettings> appSettings, IMailer mailer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _appSettings = appSettings.Value;
            _mailer = mailer;
        }
        


        [HttpGet("GetAllTickets")]
        [Authorize(Roles ="Admin")]
        public async Task<Object> GetAllTickets()
        {
            var tickets = _context.Ticket.Select(x => new
            {
                Id = x.Id,
                UserID = x.UserID,
                Title = x.Title,
                Message = x.Message,
                UserFullName = x.UserFullName,
                Priority = x.Priority,
                Status = x.Status,
                CreatedOn = x.CreatedOn,
                FixedDate = x.FixedDate,
                FeedbackMessage = x.FeedbackMessage
            }).OrderByDescending(x => x.CreatedOn).ToList();

            return tickets;
        }

        [HttpPatch("UpdateTicket")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> UpdateTicket(IUpdateTicketModel model)
        {
            var ticket = _context.Ticket.FirstOrDefault(x => x.Id == model.Id);

            if (ticket != null)
            {
                try
                {
                    ticket.Status = model.Status;
                    ticket.FixedDate = DateTime.Now;
                    ticket.FeedbackMessage = model.FeedbackMessage;

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {

                    throw new InvalidCastException(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteTicketByuserId/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Ticket>> DeleteTicketByuserId(string id)
        {
            var ticket = _context.Ticket.Where(x=> x.UserID == id).FirstOrDefault();

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
            var ticketId = _context.Ticket.Where(x=> model.Id.Contains(x.Id)).ToList();

            if (ticketId.Count == 0)
            {
                return BadRequest(new { message = "There are not any ticket find!" });
            }

            _context.Ticket.RemoveRange(ticketId);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPatch("ApproveProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ApproveProduct(IApproveProduct model)
        {
            var product = _context.Products.Where(x => x.Id == model.Id).FirstOrDefault();

            if (product != null)
            {
                try
                {
                    product.isApproved = 1; // Approved Status

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {

                    throw new InvalidCastException(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("RejectProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RejectProduct(IRejectProduct model)
        {
            var product = _context.Products.Where(x => x.Id == model.Id).FirstOrDefault();

            if (product != null)
            {
                try
                {
                    product.isApproved = 2; // Rejected Status
                    product.RejectedMessage = model.RejectedMessage;

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {

                    throw new InvalidCastException(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("RestrictedUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RestrictedUser(IRestrictedUserModel model)
        {
            var user = _context.RestrictedUsers.Where(x => x.UserID == model.UserID).FirstOrDefault();
            var userCheck = _context.User.Select(x => new { Id = x.Id, Email = x.Email }).Where(x => x.Id == model.UserID).FirstOrDefault();
            var restrictedUserProducts = _context.Products.Where(x => x.UserID == userCheck.Id).ToList();

            if (user != null)
            {
                return BadRequest(new { message = "This user has already restricted!" });
            }

            RestrictedUsers restrictedUser = new RestrictedUsers()
            {
                UserID = model.UserID,
                RestrictedDate = DateTime.Now
            };

            foreach (var products in restrictedUserProducts)
            {
                if (products.isApproved == 1)
                {
                    products.isApproved = 3;

                }

            }
            await _context.SaveChangesAsync();

            string message = "";

            message = $@"<p>Your accout has been restricted by admin!</p";

            var mailResult = _mailer.SendEmailAsync(userCheck.Email, "Restricted Message", "<h1>User Restriction Notification Mail</h1>" + "<hr>" + "<h3>" + message + "</h3>");

            _context.RestrictedUsers.Add(restrictedUser);
            await _context.SaveChangesAsync();

            return Ok(restrictedUser);

        }

        [HttpDelete("DeleteRestrictedUserById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRestrictedUserById(string id)
        {
            var restrictedUser = _context.RestrictedUsers.Where(x => x.UserID == id).FirstOrDefault();
            var userCheck = _context.User.Select(x => new { Id = x.Id, Email = x.Email }).Where(x => x.Id == id).FirstOrDefault();
            var restrictedUserProducts = _context.Products.Where(x => x.UserID == userCheck.Id).ToList();

            if (restrictedUser == null)
            {
                return BadRequest(new { message = "This user has already removed from restricted list!" });
            }
            foreach (var products in restrictedUserProducts)
            {
                if (products.isApproved == 3)
                {
                    products.isApproved = 1;
                }
            }
            await _context.SaveChangesAsync();

            string message = "";

            message = $@"<p>Your accout restriction has beed removed by admin!</p";

            var mailResult = _mailer.SendEmailAsync(userCheck.Email, "Restricted Message", "<h1>User Restriction Notification Mail</h1>" + "<hr>" + "<h3>" + message + "</h3>");

            _context.RestrictedUsers.Remove(restrictedUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Account restriction has been removed!", restrictedUser });

        }

        [HttpDelete("DeleteReview/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var reviewComment = _context.ReviewRating.Where(x => x.Id == id).FirstOrDefault();

            if (reviewComment == null)
            {
                return BadRequest(new { message = "Review cannot found in database!" });
            }
            _context.ReviewRating.Remove(reviewComment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review successfully removed!"});
        }

    }
}
