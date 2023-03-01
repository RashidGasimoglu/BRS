using BRS.DAL.Data;
using BRS.DAL.DbModels;
using BRS.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.UI.Controllers
{
    public class AuthorContactController : Controller
    {
        private readonly IGenericRepository<AuthorContact> _repository;
        private readonly AppDbContext _dbcontext;
        public AuthorContactController(IGenericRepository<AuthorContact> repository, AppDbContext dbcontext)
        {
            _repository = repository;
            this._dbcontext = dbcontext;
        }

        public IActionResult GetList()
        {
            var response = _dbcontext.AuthorContacts.Include(p => p.Author).OrderBy(p => p.AuthorId);
            return View(response);
        }
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.AuthorContacts.Include(p=>p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public IActionResult Create()
        {
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(AuthorContact item)
        {
            if (ModelState.IsValid)
            {
                _repository.AddItem(item);
                return RedirectToAction("GetList");
            }
            else
            {
                ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
                ViewBag.Error = "Please doublecheck your information";
                return View();
            }
        }

        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.AuthorContacts.Include(p => p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View(response);
        }
        [HttpPost]
        public IActionResult UpdateAuthorContact(int id, [Bind("Id,ContactNumber,Address,AuthorId")] AuthorContact item)
        {
            var response = _repository.GetById(id);
            response.AuthorId = item.AuthorId;
            response.ContactNumber = item.ContactNumber;
            response.Address = item.Address;
            _repository.Update(response);
            return RedirectToAction("GetList");
        }


        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.AuthorContacts.Include(p => p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View(response);
        }
        public IActionResult DeleteAuthorContact(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
    }
}
