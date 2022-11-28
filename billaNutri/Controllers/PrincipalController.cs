using billaNutri.Models.Banco_de_Dados;
using billaNutri.Models.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace billaNutri.Controllers
{
    [Authorize]
    public class PrincipalController : ConfigController
    {
        public PrincipalController(Context context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult Main()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            DadosClienteVm vm = new DadosClienteVm();

            return View(new DadosClienteVm { Clientes = ListarCliente(vm), Enderecos = ListarEnderecoCliente(vm)});
        }

        private List<CLIENTE> ListarCliente(DadosClienteVm vm)
        {
            List<CLIENTE> clientes = new List<CLIENTE>();

            clientes = _context.CLIENTE.Where(x => x.ID_USUARIO == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();

            return clientes;
        }

        private List<ENDERECO> ListarEnderecoCliente(DadosClienteVm vm)
        {
            List<ENDERECO> endereco = new List<ENDERECO>();

            var select = _context.CLIENTE.FirstOrDefault(x => x.ID_USUARIO == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if(select != null)
            {
                endereco = _context.ENDERECO.Where(x => x.ID_CLIENTE == select.IDCLIENTE).ToList();
            }

            return endereco;
        }

        [HttpPost]
        public IActionResult Main(DadosClienteVm vm)
        {
            return InsertDadosCliente(vm);
        }

        public JsonResult InsertDadosCliente(DadosClienteVm vm)
        {
            try
            {

                CLIENTE cliente = new CLIENTE();
                cliente.ID_USUARIO = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                cliente.NOME = vm.Nome;
                cliente.SOBRENOME = vm.SobreNome;
                cliente.CPF = vm.Cpf;
                cliente.CELULAR = vm.Celular;

                _context.CLIENTE.Add(cliente);
                _context.SaveChanges();

                ENDERECO endereco = new ENDERECO();
                endereco.ID_CLIENTE = cliente.IDCLIENTE;
                endereco.LOGRADOURO = vm.Logradouro;
                endereco.CEP = vm.Cep;
                endereco.BAIRRO = vm.Bairro;
                endereco.CIDADE = vm.Cidade;

                _context.ENDERECO.Add(endereco);
                _context.SaveChanges();

                return Json(new { status = "success", message = "Dados Cadastrados com sucesso !" });

            }catch(Exception e)
            {

                return Json(new { status = "error", message = "Ocorreu um erro, por gentileza entrar em contato com o suporte !" });
            }

        }

        [HttpGet]
        public IActionResult Servicos()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Agendamento()
        {
            DadosAgendamentoVm vm = new DadosAgendamentoVm();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Agendamento(DadosAgendamentoVm vm)
        {
            return InserirAgendamento(vm);
        }

        private JsonResult InserirAgendamento(DadosAgendamentoVm vm)
        {
            string mensagem;

            try
            {
                AGCLIENTE ag = new AGCLIENTE();
                ag.ID_USUARIO = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                ag.DATAAGCL = vm.DataAg;
                ag.HORAAGCL = vm.HoraAg;
                ag.FORMAPAGAMENTO = vm.FormaPgto;

                _context.AGCLIENTE.Add(ag);
                _context.SaveChanges();

                return InserirPagamento(vm,ag.IDAGCLIENTE);

            }
            catch(Exception e)
            {
                return Json(new { status = "error", message = "Ocorreu um erro, por gentileza entrar em contato com o suporte!" });
            }

        }

        public JsonResult AlterarDadosCliente(DadosClienteVm vm)
        {
            try
            {

                var dados = _context.CLIENTE.FirstOrDefault(x => x.ID_USUARIO.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier));

                dados.NOME = vm.Nome;
                dados.SOBRENOME = vm.SobreNome;
                dados.CELULAR = vm.Celular;
                dados.CPF = vm.Cpf;

                var endereco = _context.ENDERECO.FirstOrDefault(x => x.ID_CLIENTE == dados.IDCLIENTE);

                endereco.CEP = vm.Cep;
                endereco.BAIRRO = vm.Bairro;
                endereco.LOGRADOURO = vm.Logradouro;
                endereco.CIDADE = vm.Cidade;

                _context.SaveChanges();

                return Json(new { status = "success", message = "Dados alterados com sucesso !" });

            }
            catch(Exception e)
            {
                return Json(new { status = "error", message = "Ocorreu um erro, por gentileza entrar em contato com o suporte!" });
            }
        }

        [HttpGet]
        public IActionResult Cartao(int idAg)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            DadosCartaoVm vm = new DadosCartaoVm
            {
                IdAg= idAg
            };

            return View(vm);
        }

        [HttpPost]
        public JsonResult InserirPagementoCartao(int idAg)
        {
            try
            {
                var select = _context.AGCLIENTE.FirstOrDefault(x => x.IDAGCLIENTE == idAg);

                STATUSPAGAMENTO st = new STATUSPAGAMENTO();
                st.ID_AGCLIENTE = idAg;
                st.SATTUSDESQ = select.FORMAPAGAMENTO;
                st.COMPROVANTE = "imagem";
                st.VALOR = 150.00;

                _context.STATUSPAGAMENTO.Add(st);
                _context.SaveChanges();

                return Json(new { status = "success", message = "Pagamento realizado com sucesso !" });

            }
            catch(Exception e) 
            {
                return Json(new { status = "error", message = "Ocorreu um erro , por gentileza entrar em contato com o suporte !" });
            }
        }

        private JsonResult InserirPagamento(DadosAgendamentoVm vm, int idAgCliente)
        {
            try
            {               
                if(vm.FormaPgto == 1)
                {
                    STATUSPAGAMENTO st = new STATUSPAGAMENTO();
                    st.ID_AGCLIENTE = idAgCliente;
                    st.SATTUSDESQ = vm.FormaPgto;
                    st.COMPROVANTE = "imagem";
                    st.VALOR = 150.00;

                    _context.STATUSPAGAMENTO.Add(st);
                    _context.SaveChanges();

                    return Json(new { status = "success", message = "Pagamento realizado com sucesso !" });
                }
                else
                {
                    return Json(new { status = "success", message = "Você será redirecionado para página de pagamento via cartão de crédito !", idAg = idAgCliente});
                }
                
            }catch(Exception e)
            {
                return Json(new { status = "error", message = "Ocorreu um erro, por gentileza entrar em contato com o suporte ! !" });
            }

        }

        public ActionResult Logoff()
        { 
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<JsonResult> GetEnd(string numeroCep)
        {
            //CASO O NÚMERO SEJA INFERIOR A NOVE
            if (string.IsNullOrEmpty(numeroCep) || numeroCep.Length < 9)
            {
                return Json(new { retorno = true });
            }

            string retorno;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://viacep.com.br/ws/" + numeroCep + "/json/"))
                {
                    retorno = await response.Content.ReadAsStringAsync();
                }
            }

            return Json(retorno);

        }

        [HttpGet]
        public IActionResult Comprovante()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            DadosComprovanteVm vm = new DadosComprovanteVm();

            return View();
        }

    }
}
