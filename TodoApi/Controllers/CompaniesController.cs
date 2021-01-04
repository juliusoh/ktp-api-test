using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyContext _context;

        public CompaniesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyItem>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<CompanyItem>> GetCompanyItem([FromRoute] long id)
        {
            var companyItem = await _context.Companies.FindAsync(id);

            if (companyItem == null)
            {
                return NotFound();
            }

            return companyItem;
        }

        // GET: api/Companies/adigitalearth
        [HttpGet("{username}")]
        public async Task<ActionResult<CompanyItem>> GetCompanyByUserName([FromRoute] string UserName)
        {
            var companyUserName = await _context.Companies.FindAsync(UserName);

            if (companyUserName == null)
            {
                return NotFound();
            }

            return companyUserName;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyItem(long id, CompanyItem companyItem)
        {
            if (id != companyItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyItem>> PostCompanyItem(CompanyItem companyItem)
        {
            _context.Companies.Add(companyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompanyItem), new { id = companyItem.Id }, companyItem);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyItem(long id)
        {
            var companyItem = await _context.Companies.FindAsync(id);
            if (companyItem == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(companyItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyItemExists(long id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
