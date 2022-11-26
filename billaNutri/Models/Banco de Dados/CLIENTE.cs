using billaNutri.Models.Home;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace billaNutri.Models.Banco_de_Dados
{
    public class CLIENTE
    {
        [Key]
        public int IDCLIENTE { get; set; }

        [ForeignKey("USUARIO")]
        public int ID_USUARIO { get; set; }
        public string? NOME { get; set; }
        public string? SOBRENOME { get; set; }
        public string? CPF { get; set; }
        public string? CELULAR { get; set; }
    }
}
