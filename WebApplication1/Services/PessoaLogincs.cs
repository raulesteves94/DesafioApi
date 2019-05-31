using System.Linq;
using WebApplication1.Controllers;

namespace WebApplication1.Services
{
    public class PessoaLogin
    {
        public static bool Login(string username, string password)
        {
            return PessoasController.repositorio.GetAll().Any(p => p.CPF.Replace(".", "").Replace("-", "") == username.Replace(".", "").Replace("-", "") && p.Senha == password);                    
        }
    }
}