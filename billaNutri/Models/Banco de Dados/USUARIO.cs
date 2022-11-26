using System.ComponentModel.DataAnnotations;

namespace billaNutri.Models.Home
{
    public class USUARIO
    {
        [Key]
        public int IDUSUARIO { get; set; }
        public short GRUPO { get; set; }
        public string? EMAIL { get; set; }
        public string? SENHA { get; set; }
        public short ATIVO { get; set; }
        public DateTime DT_CRIACAO { get; set; }

    }
}
