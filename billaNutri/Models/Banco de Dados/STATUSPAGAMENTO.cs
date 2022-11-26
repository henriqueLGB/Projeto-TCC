using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace billaNutri.Models.Banco_de_Dados
{
    public class STATUSPAGAMENTO
    {
        [Key]        
        public int IDSTATUSPAGAMENTO { get; set; }

        [ForeignKey("AGCLIENTE")]
        public int ID_AGCLIENTE { get; set; }
        public short SATTUSDESQ { get; set; }
        public string COMPROVANTE { get; set; }
        public double VALOR { get; set; }

    }
}
