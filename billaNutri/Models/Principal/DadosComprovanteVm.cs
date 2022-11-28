using billaNutri.Models.Banco_de_Dados;

namespace billaNutri.Models.Principal
{
    public class DadosComprovanteVm
    {
        public int idUsuario { get; set; }
        public List<STATUSPAGAMENTO> ListaPagamentos { get; set; }
        public STATUSPAGAMENTO ListaPagamento { get; set; }
        public List<AGCLIENTE> AGCLIENTES { get; set; }
        public AGCLIENTE AGCLIENTE { get; set; }
    }
}
