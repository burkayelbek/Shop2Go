using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class MailController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly ApplicationSettings _appSettings;
        private readonly IMailer _mailer;
        public MailController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context, IOptions<ApplicationSettings> appSettings, IMailer mailer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _appSettings = appSettings.Value;
            _mailer = mailer;
    }

        [HttpGet("SendTestMail")]
        public async Task<IActionResult> SendTestMail()
        {
            var result =  _mailer.SendEmailAsync("burkay.elbek@gmail.com", "Test", "Test body");
            return NoContent();
        }

        [HttpPost("SendMailToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendMailToUser(IUserMailModel model)
        {
            var user = _context.User.Where(x => x.Id == model.Id).FirstOrDefault();

            if (user == null)
            {
                return BadRequest(new {message = "User could not found!"});
            }

            var result = _mailer.SendEmailAsync(model.Email, "Shop2Go Mail Information", model.Content);

            return Ok();
        }

    }
}
