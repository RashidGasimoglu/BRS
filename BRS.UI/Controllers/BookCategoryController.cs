using BRS.DAL.DbModels;
using BRS.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.UI.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly IGenericRepository<BookCategory> _repository;
        public BookCategoryController(IGenericRepository<BookCategory> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<BookCategory>> GetList()
        {
            var response = _repository.GetList();
            return View(response);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult<BookCategory> Create(BookCategory item)
        {
            _repository.AddItem(item);
            return RedirectToAction("GetList");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public IActionResult UpdateBookCategory(int id, [Bind("Id,Name")] BookCategory item)
        {
            var response = _repository.GetById(id);
            response.Name = item.Name;
            _repository.Update(response);
            return RedirectToAction("GetList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpDelete]
        public IActionResult DeleteBookCategory(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
    }
}
