using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Db.DAL.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly Jifas_MagangContext _context;

        public InvoiceController(Jifas_MagangContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            var invoices = _context.invoice.ToList();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _context.invoice.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] invoice newInvoice)
        {
            if (newInvoice == null)
            {
                return BadRequest();
            }
            _context.invoice.Add(newInvoice);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetInvoice), new { id = newInvoice.id }, newInvoice);
        }
    }
}