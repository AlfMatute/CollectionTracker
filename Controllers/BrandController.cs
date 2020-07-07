using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CollectionTrackerMVC.Models;
using CollectionTrackerMVC.ViewModels;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id:int}")]
        public ActionResult<BrandViewModel> Get(int id)
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
                _logger.LogError($"Error when getting the Brand: {ex.Message}");
                return BadRequest($"Error when getting the Brand: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<BrandViewModel>> Get()
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
                _logger.LogError($"Error when getting active brands {ex.Message}");
                return BadRequest($"Error when getting active brands {ex.Message}");
            }
        }

        // GET: BranchController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: BranchController/Create
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Update([FromBody]BrandViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var updatebrand = _mapper.Map<BrandViewModel, Brand>(model);
                    _context.Update(updatebrand);
                    if (_context.SaveChanges() == 0)
                    {
                        return Ok(_mapper.Map<Brand, BrandViewModel>(updatebrand));
                    }
                    else
                    {
                        return BadRequest("Failed to update the brand");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error when updating the brand: {ex.Message}");
                return BadRequest($"Error when updating the brand: {ex.Message}");
            }
        }

        // GET: BranchController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]BrandViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var NewBrand = _mapper.Map<BrandViewModel, Brand>(model);
                    _context.Add(NewBrand);
                    if (_context.SaveChanges() == 0)
                    {
                        return Created($"/api/Brand/{NewBrand.BrandId}", _mapper.Map<Brand, BrandViewModel>(NewBrand));
                    }
                    else
                    {
                        return BadRequest("Failed to save the new brand");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error when saving the new brand: {ex.Message}");
                return BadRequest($"Error when saving the new brand: {ex.Message}");
            }
        }

        // GET: BranchController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: BranchController/Delete/5
        [HttpDelete("{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var deletebrand = _context.Brands.Where(b => b.BrandId == id).FirstOrDefault();
                if(deletebrand != null)
                {
                    _context.Remove(deletebrand);
                    if(_context.SaveChanges() == 0)
                    {
                        return Ok(_mapper.Map<Brand, BrandViewModel>(deletebrand));
                    }
                    else
                    {
                        return BadRequest("Failed to delete the brand");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error when deleting the brand: {ex.Message}");
                return BadRequest($"Error deleting the brand: {ex.Message}");
            }
        }
    }
}
