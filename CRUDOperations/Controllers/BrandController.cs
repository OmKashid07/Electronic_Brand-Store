using CRUDOperations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace CRUDOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbcontext;
        public BrandController(BrandContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("GetAllBrand")]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            try
            {
                if (_dbcontext.Brands == null)
                {
                    return NotFound();
                }
                var response = await _dbcontext.Brands.ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ById{id}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {

            if (_dbcontext.Brands == null)
            {
                return NotFound();
            }
            var Brand = await _dbcontext.Brands.FindAsync(id);
            if (Brand == null)
            {
                return NotFound();
            }
            return Brand;
        }

        [HttpPost("AddBrand")]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            brand.Id = 0;
            _dbcontext.Brands.Add(brand);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, brand);

        }
        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }

        private bool BrandAvailable(int id)
        {
            return (_dbcontext.Brands?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteBrandById{id}")]

        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_dbcontext.Brands == null)
            {
                return NotFound();
            }

            var brand = await _dbcontext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound(); 

             }
            _dbcontext.Brands.Remove(brand);
            await _dbcontext.SaveChangesAsync();

            return Ok();


        }
    }
        
}
