using billaNutri.Models.Banco_de_Dados;

namespace billaNutri.Models.Principal
{
    public class DadosClienteVm
    {
        public int IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public string? Cpf { get; set; }    
        public string? Celular { get; set; }
        public string? Logradouro { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public List<CLIENTE> Clientes { get; set; }
        public List<ENDERECO> Enderecos { get; set; }

    }
}
