using System;
using APICatalogo.Entitis;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
			
		}
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Produto> Produtos { get; set; }
        }
}
