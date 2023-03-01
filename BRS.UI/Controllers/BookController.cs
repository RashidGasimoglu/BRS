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
    public class BookController : Controller
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly AppDbContext _dbcontext;
        public BookController(IGenericRepository<Book> repository, AppDbContext dbcontext)
        {
            _repository = repository;
            this._dbcontext = dbcontext;
        }

        public IActionResult GetList()
        {
            var response = _dbcontext.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Author).ToList();
            return View(response);
        }
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_dbcontext.BookCategories.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(_dbcontext.Publishers.ToList(), "Id", "Name");
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book item)
        {
            if (ModelState.IsValid)
            {
                _repository.AddItem(item);
                return RedirectToAction("GetList");
            }
            else
            {
                ViewBag.Category = new SelectList(_dbcontext.BookCategories.ToList(), "Id", "Name");
                ViewBag.Publisher = new SelectList(_dbcontext.Publishers.ToList(), "Id", "Name");
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
            var response = _dbcontext.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.Category = new SelectList(_dbcontext.BookCategories.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(_dbcontext.Publishers.ToList(), "Id", "Name");
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View(response);
        }
        [HttpPost]
        public IActionResult UpdateBook(int id, [Bind("Id,Title,CategoryId,PublisherId,AuthorId")] Book item)
        {
            var response = _repository.GetById(id);
            response.Title = item.Title;
            response.CategoryId = item.CategoryId;
            response.PublisherId = item.PublisherId;
            response.AuthorId = item.AuthorId;
            _repository.Update(response);
            return RedirectToAction("GetList");
        }


        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Author).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.Category = new SelectList(_dbcontext.BookCategories.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(_dbcontext.Publishers.ToList(), "Id", "Name");
            ViewBag.Author = new SelectList(_dbcontext.Authors.ToList(), "Id", "Name");
            return View(response);
        }
        public IActionResult DeleteBook(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
    }
}
