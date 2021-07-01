using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Category.ToListAsync();    
        }

        [HttpGet("{name}")]
        [Authorize]
        public async Task<ActionResult> GetCategoryByName(string name)
        {
            var category = _context.Category.FirstOrDefault(x => x.Name == name);

            if (category ==null)
            {
                return BadRequest( new { message = "Category could not found!" });

            }
            return Ok(category);
            
        }
       
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> AddCategory(IAddCategory model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var categoryName = _context.Category.FirstOrDefault(x => x.Name == model.Name);

            if (String.IsNullOrEmpty(model.Name))
            {
                return BadRequest(new { message = "This field cannot be left blank!" });

            }
            if (categoryName != null)
            {
                return BadRequest(new { message = "This category is already exist!" });
            }
            Category newCategory = new Category()
            {
                Name = model.Name,
                MatIconName = model.MatIconName
            };
            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            return Ok(newCategory);

        }
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditCategory(IAddCategory model)
        {
            var USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var categoryNameControl = _context.Category.FirstOrDefault(x => x.Name == model.Name);

            var category = _context.Category.FirstOrDefault(x => x.Id == model.Id);

            if (category == null)
            {
                return BadRequest(new { message = "Category could not found!" });
            }
            else
            {
                  
                if (category.Name == model.Name)
                {
                    category.Name = category.Name;
                    category.MatIconName = model.MatIconName;
                    _context.Category.Update(category);
                    var result = await _context.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    try
                    {

                        if (categoryNameControl != null)
                        {
                            return BadRequest(new { message = "This category is already exist!" });
                        }
                        else
                        {
                            category.Name = model.Name;
                            category.MatIconName = model.MatIconName;

                            _context.Category.Update(category);
                            var result = await _context.SaveChangesAsync();
                            return Ok(result);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
             
            }
           

        }
   
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            string USERID = User.Claims.First(x => x.Type == "UserID").Value;

            var category = _context.Category.Where( x=> x.Id == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

    }
}
