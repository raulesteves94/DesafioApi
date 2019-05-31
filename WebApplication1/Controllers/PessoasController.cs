using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Repositories;
using WebApplication1.ErrorHandling;
using WebApplication1.Repositories.Contracts;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PessoasController : ApiController
    {
        public static readonly IPessoaRepositorio repositorio = new PessoaRepositorio();

        public IEnumerable<Pessoa> GetAllPessoas()
        {
            return repositorio.GetAll();
        }

        public Pessoa GetPessoa(int id)
        {
            Pessoa pessoa = repositorio.Get(id);
            if (pessoa == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return pessoa;
        }

        public IEnumerable<Pessoa> GetPessoasPorUF(string uf)
        {
            return repositorio.GetAll().Where(p => string.Equals(p.UF, uf, StringComparison.OrdinalIgnoreCase));
        }

        [ValidateModel]
        public HttpResponseMessage PostPessoa(Pessoa pessoa)
        {
            pessoa = repositorio.Add(pessoa);
            var response = Request.CreateResponse(HttpStatusCode.Created, pessoa);

            string uri = Url.Link("DefaultApi", new { codigo = pessoa.Codigo });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutPessoa(int codigo, Pessoa pessoa)
        {
            pessoa.Codigo = codigo;
            if (!repositorio.Update(pessoa))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.Accepted, pessoa);

            string uri = Url.Link("DefaultApi", new { codigo = pessoa.Codigo });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void DeletePessoa(int codigo)
        {
            Pessoa pessoa = repositorio.Get(codigo);

            if (pessoa == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repositorio.Remove(codigo);
        }
    }
}
