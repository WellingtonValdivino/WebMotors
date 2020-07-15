using System.ComponentModel.DataAnnotations;

namespace WebMotors.WebAPI.Dtos
{
    public class AnuncioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Campo obrigatório")]
        public string Marca { get; set; }

        [Required(ErrorMessage="Campo obrigatório")]
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }
    }
}