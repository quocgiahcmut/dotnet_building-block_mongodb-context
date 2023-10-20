using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApiTest.Domain.Models;
using WebApiTest.Infrastructure;

namespace WebApiTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly TestDBContext _context;

    public ProductController(TestDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var result = await _context.Producs.Find(_ => true).ToListAsync();

        return Ok(result);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _context.Producs.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Product product)
    {
        await _context.Producs.InsertOneAsync(product);

        return NoContent();
    }

    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult> Update(string id,[FromBody] Product updatedProduct)
    {
        var product = await _context.Producs.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        updatedProduct.Id = product.Id;

        await _context.Producs.ReplaceOneAsync(p => p.Id == id, updatedProduct);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<ActionResult> Delete(string id)
    {
        var product = await _context.Producs.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        await _context.Producs.DeleteOneAsync(p => p.Id == id);
        return NoContent();
    }
}
