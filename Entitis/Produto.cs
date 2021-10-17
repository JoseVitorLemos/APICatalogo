using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.Validations;

namespace APICatalogo.Entitis
{

    [Table("Produtos")]
	public class Produto
	{

	[Key]
    public int ProdutoId { get; set; }

	[Required(ErrorMessage = "O nome é obrigatório")]
	[MaxLength(80)]
    [PrimeiraLetraMaiuscula]
    public string Nome { get; set; }
 
	[Required]
	[MaxLength(30)]       
	public string Descricao { get; set; }
        
	[Required]
	public decimal Preco { get; set; }
	
	[Required]
	[MaxLength(300)]
	public string ImagemUrl { get; set; }
	public float Estoque { get; set; }
	public DateTime DataCadastro { get; set; }

	public Categoria Categoria { get; set; }
	public int CategoriaId { get; set; }
    }
}

