using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CollectionTrackerMVC.Models;
using CollectionTrackerMVC.ViewModels;
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
        private readonly IMapper _mapper;
        public BrandController(ILogger<BrandController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        // GET: BranchController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("{id:int}")]
        public ActionResult<BrandViewModel> GetById(int id)
        {
            try 
            {
                var brand = _context.Brands.Where(b => b.BrandId == id).FirstOrDefault();
                if(brand != null)
                {
                    return Ok(_mapper.Map<Brand, BrandViewModel>(brand));
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get the Brand: {ex.Message}");
                return BadRequest($"Failed to get the Brand: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<BrandViewModel>> GetBrands()
        {
            try
            {
                var brands = _context.Brands.ToList();
                if(brands != null)
                {
                    return Ok(_mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failead to get active brands {ex.Message}");
                return BadRequest($"Failead to get active brands {ex.Message}");
            }
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
        public ActionResult Post([FromBody]BrandViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var NewBrand = _mapper.Map<BrandViewModel, Brand>(model);
                    _context.Add(NewBrand);
                    _context.SaveChanges();
                    return Created($"/api/Brand/{NewBrand.BrandId}", NewBrand);
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
