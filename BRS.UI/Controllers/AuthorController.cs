using BRS.DAL.DbModels;
using BRS.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRS.UI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IGenericRepository<Author> _repository;
        public AuthorController(IGenericRepository<Author> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetList()
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
        public ActionResult<Author> Create(Author item)
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
        
        public IActionResult UpdateAuthor(int id, [Bind("Id,Name")] Author item)
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
        public IActionResult DeleteAuthor(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
    }
}
