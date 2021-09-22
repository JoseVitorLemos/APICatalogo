using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Entitys;

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
            return _context.Produtos.ToList();    
        }
    }



}


