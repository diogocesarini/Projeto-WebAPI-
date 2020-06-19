using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 1 a 100 caracteres!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 1 a 200 caracteres!")]
        public string Sinopse { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 1 a 20 caracteres!")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(4, ErrorMessage = "Este campo deve conter entre 1 a 4 caracteres!")]
        public string Ano { get; set; }

        public bool IsDisponivel { get; set; }

        public bool Ativo { get; set; }
    }
}
