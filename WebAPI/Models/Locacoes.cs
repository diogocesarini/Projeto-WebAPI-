using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Locacoes
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "Preço deve ser Maior que 0!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public DateTimeOffset DataSaida { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public DateTimeOffset DataEntrega { get; set; }

        public DateTimeOffset? DataEntregada { get; set; }

        public int IdFilme { get; set; }

        public int IdCliente { get; set; }

        public Filme Filme { get; set; }

        public Cliente Cliente { get; set; }
    }
}
