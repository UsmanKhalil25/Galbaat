using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galbaat.Data;
using Galbaat.Models;
using Microsoft.AspNetCore.Authorization;

namespace Galbaat.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,AppUserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.TimeStamp = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", post.AppUserId);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string content)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Content = content;

            _context.Update(post);
            await _context.SaveChangesAsync();

            return Ok(); 
        }



        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
