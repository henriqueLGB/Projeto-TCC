using billaNutri.Models;
using billaNutri.Models.Banco_de_Dados;
using billaNutri.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Diagnostics;
using System.Security.Claims;

namespace billaNutri.Controllers
{
    public class HomeController : ConfigController
    {
        public HomeController(Context context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioVm vm)
        {
            return Login(vm);
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioVm vm)
        {
            if(VerificaDadosLogin(vm) == false)
            {
                ViewBag.Mesagem = "Você deve preencher os dados !";
                return View();
            }
            return CriarUsuario(vm);
        }

        private JsonResult CriarUsuario(UsuarioVm vm)
        {

            var select = _context.USUARIO.FirstOrDefault(x => x.EMAIL == vm.Email);

            //VERIFICA SE NÃO TEVE RETORNO 
            if (select != null)
            {
                return Json(new { status = "warning", message = "Já existe um usuário com esse e-mail cadastrado !" });
            }

            try
            {
                USUARIO usuario = new USUARIO();
                usuario.EMAIL = vm.Email;
                usuario.SENHA = Criptografia(vm.Senha);
                usuario.GRUPO = 2; //2 - CLIENTE
                usuario.ATIVO = 1;
                usuario.DT_CRIACAO = DateTime.Now;

                _context.USUARIO.Add(usuario);
                _context.SaveChanges();


                return Json(new { status = "success", message = "Cadastro realizado com sucesso !" });
            }
            catch(Exception e)
            {

                return Json(new { status = "error", message = "Ocorreu um erro, por gentileza entrar em contato com o suporte !" });
            }
        }

        private bool VerificaDadosLogin(UsuarioVm vm)
        {

            if (vm.Email == null || vm.Senha == null)
            {
                return false;
            }

            return true;
        }

        private IActionResult Login(UsuarioVm vm)
        {
            var select = _context.USUARIO.FirstOrDefault(x => x.EMAIL == vm.Email && x.SENHA == Criptografia(vm.Senha));

            //VERIFICA SE NÃO TEVE RETORNO 
            if (select == null)
            {
                ViewBag.Mesagem = "Usuário ou Senha inválido !";
                return View();
            }

            EfetuarIdentity(select.EMAIL,select.IDUSUARIO);

            var verify = _context.CLIENTE.FirstOrDefault(x => x.ID_USUARIO.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (verify != null)
            {
                return RedirectToAction("Servicos", "Principal");
            }

            return RedirectToAction("Main", "Principal");
        }

        private void EfetuarIdentity(string email,int idUsuario)
        {
            CriarIdentity(idUsuario, email);
        }

        public IActionResult EsqueceuSenha()
        {
            UsuarioVm vm = new UsuarioVm();
            return View(vm);
        }

    }
}