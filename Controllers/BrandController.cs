using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionTrackerMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollectionTrackerMVC.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;
        private readonly ApplicationDbContext _context;

        public BrandController(ILogger<BrandController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        // GET: BranchController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BranchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BranchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BranchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post([FromBody]Brand model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(model);
                    _context.SaveChanges();
                    return Created($"/api/Brand/{model.BrandId}", model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Failed to save the new brand: {ex.Message}");
                return BadRequest($"Failed to save the new brand: {ex.Message}");
            }
        }

        // GET: BranchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
