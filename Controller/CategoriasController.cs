using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Entitys;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
	public class CategoriasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        
        public CategoriasController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {
            return _context.Categorias.Include(x => x.Produtos).ToList();    
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();    
        }

        [HttpGet("{id}",Name ="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(prop => prop.CategoriaId == id);

            if(categoria == null)
            {
                return NotFound();
            }
            return categoria; 
        }

        [HttpPost]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", 
            new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Categoria categoria)
        {
            if(id != categoria.CategoriaId)
            {
                return BadRequest();
            };

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(); 
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(prop => prop.CategoriaId == id);
            
            if(categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        }
    }
}


