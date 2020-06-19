using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(14, ErrorMessage = "Este campo deve conter no máximo 14 caracteres!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(80, ErrorMessage = "Este campo deve conter entre 1 a 80 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 1 a 60 caracteres!")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(7, ErrorMessage = "Este campo deve conter entre 1 a 7 caracteres!")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 1 a 30 caracteres!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Telefone { get; set; }

        public bool Ativo { get; set; }

    }
}
