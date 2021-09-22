using System;
using APICatalogo.Entitys;
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
