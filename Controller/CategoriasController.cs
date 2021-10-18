using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Entitis;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 
using APICatalogo.Services;
using Microsoft.Extensions.Logging;

namespace APICatalogo.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
	public class CategoriasController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        
        public CategoriasController(DatabaseContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico, string nome)
        {
           return meuservico.Saudacao(nome);
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
        { 
            try
            {
               return await _context.Categorias.AsNoTracking().ToListAsync();
            } 
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar obter as Categorias do banco de dados");
            }   
       }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaProdutosAsync()
        {
            _logger.LogInformation("################## GET api/categorias/produtos ##################");

            return await _context.Categorias.Include(x => x.Produtos).ToListAsync();    
        }

        [HttpGet("{id}",Name ="ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetAsync(int id)
        {
            _logger.LogInformation($"################## GET api/categorias/produtos/id = {id} ##################");

            try
            {
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(prop => prop.CategoriaId == id);

                if(categoria == null)
                {
                    return NotFound($"A categoria com Id={id} não foi encontrada");
                }
                return categoria; 
            } 
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar obter a Categorias do banco de dados");
            }    
        }

        [HttpPost]
        public ActionResult Post([FromBody]Categoria categoria)
        {
            try 
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", 
                new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar criar uma nova categoria");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Categoria categoria)
        {
            try 
            {
                if(id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com Id={id}");
                };

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Categoria alterada com sucesso"); 
            }
            catch (Exception)
            {
                return StatusCode(500,$"Erro ao tentar alterar a categoria com Id={id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(prop => prop.CategoriaId == id);
                
                if(categoria == null)
                {
                    return NotFound($"Não foi possível encontrar a categoria com Id={id}");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao tentar deletar a categoria");
            }
        }
    }
}


