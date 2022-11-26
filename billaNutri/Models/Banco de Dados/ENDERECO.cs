using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace billaNutri.Models.Banco_de_Dados
{
    public class ENDERECO
    {
        [Key]
        public int IDENDERECO { get; set; }

        [ForeignKey("CLIENTE")]
        public int ID_CLIENTE { get; set; }
        public string? LOGRADOURO { get; set; }
        public string? BAIRRO { get; set; }
        public string? CIDADE { get; set; }
        public string? CEP { get; set; }
    }
}
