using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace billaNutri.Models.Banco_de_Dados
{
    public class AGCLIENTE
    {
        [Key]
        public int IDAGCLIENTE { get; set; }

        [ForeignKey("USUARIO")]
        public int ID_USUARIO { get; set; }
        public DateTime DATAAGCL { get; set; }
        public short HORAAGCL { get; set; }
        public short FORMAPAGAMENTO { get; set; }
    }
}
