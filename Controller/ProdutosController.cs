using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Entitys;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Filter;

namespace APICatalogo.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
	public class ProdutosController : ControllerBase
    {
        private readonly DatabaseContext _context;
        
        public ProdutosController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                return _context.Produtos.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error ao buscar produto");
            }            
        }

        [HttpGet("{id}",Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(prop => prop.ProdutoId == id);
            return produto;
            }
            catch (Exception)
            {
                return StatusCode(500, "Não foi possível encontrar o produto");
            }
         }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            try
            {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Não foi possível adicionar o produto");
            } 
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] Produto produto)
        {
            try 
            {
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao alterar produto"); 
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
            var produto = _context.Produtos.FirstOrDefault(prop => prop.ProdutoId == id);
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao deletar produto");
            }
        }
    }
}


