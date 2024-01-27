using Lab3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Text.Json;

namespace Lab3.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarController : Controller
    {
        private readonly ICarService _CarService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CarController(ICarService CarService, IDateTimeProvider dateTimeProvider)
        {
            _CarService = CarService;
            _dateTimeProvider = dateTimeProvider;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_CarService.FindAll());
        }

        public IActionResult PagedIndex(int page = 1, int size = 1)
        {
            if (size < 1) return BadRequest();
            return View(_CarService.FindPage(page, size));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car opel)
        {
            opel.PublicationDate = _dateTimeProvider.dateNow();
            if (ModelState.IsValid)
            {
                _CarService.Add(opel);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_CarService.FindById(id));
        }


        [HttpPost]
        public IActionResult Update(Car opel)
        {
            if (ModelState.IsValid)
            {
                _CarService.Update(opel);
                return RedirectToAction("PagedIndex");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_CarService.FindById(id));
        }


        [HttpPost]
        public IActionResult Delete(Car opel)
        {
            _CarService.Delete(opel.Id);
            return RedirectToAction("PagedIndex");
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _CarService.FindById(id);
            return model is null ? NotFound() : View(model);
        }
    }

}
