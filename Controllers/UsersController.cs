using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using newProject.Models;

namespace newProject.Controllers
{
    public class UsersController : Controller
    {
        //DB Context
        private readonly MyDbContext _context;

        public UsersController(){
            _context = new MyDbContext();
        }



        // GET: Users
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usr =await _context.Users.FindAsync(id);
            if(usr==null){
                return NotFound();
            }
            return View(usr);
            
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
            
               var newUser = CastToUser(collection);
                // TODO: Add insert logic here
                
               _context.Add(newUser);
               await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usr =await _context.Users.FindAsync(id);
            if(usr==null){
                return NotFound();
            }
            return View(usr);
            
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                User usr = await _context.Users.FindAsync(id);

                User newUsr = CastToUser(collection);

                usr.UserName = newUsr.UserName;
                usr.Email=newUsr.Email;
                usr.Password=newUsr.Password;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
           
            var usr =await _context.Users.FindAsync(id);
            if(usr==null){
                return NotFound();
            }
            return View(usr);
            
         
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public static User CastToUser (IFormCollection collection){
               User newUser = new User();
               newUser.UserName=collection["UserName"];
               newUser.Email=collection["Email"];
               newUser.Password=collection["Password"];
               return newUser;
        }
    }
}