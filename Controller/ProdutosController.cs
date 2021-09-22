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
	public class ProdutosController : ControllerBase
    {

        private readonly DatabaseContext _context;
        
        public ProdutosController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _context.Produtos.AsNoTracking().ToList();    
        }

        [HttpGet("{id}",Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(prop => prop.ProdutoId == id);

            if(produto == null)
            {
                return NotFound();
            }
            return produto; 
         }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId }, produto);
        }
    }
}


