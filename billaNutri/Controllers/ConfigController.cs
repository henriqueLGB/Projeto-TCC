using billaNutri.Models.Banco_de_Dados;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace billaNutri.Controllers
{
    public class ConfigController : Controller
    {
        public readonly Context _context;

        public ConfigController(Context context)
        {
            _context = context;
        }

        public async void CriarIdentity(int id, string usuario)
        {
            List<Claim> acesso = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,id.ToString()),
                new Claim(ClaimTypes.Name,usuario)
            };

            var identity = new ClaimsIdentity(acesso, "Identity.Login");
            var userPrincipal = new ClaimsPrincipal(new[] { identity });

            await HttpContext.SignInAsync(userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.Now.AddHours(1)
                }
            );
        }

        public static string Criptografia(string texto)
        {
            MD5 md5Hash = MD5.Create();

            Byte[] dado = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < dado.Length; i++)
            {
                builder.Append(dado[i].ToString("x2"));
            }

            return builder.ToString();

        }
    

    }
}
