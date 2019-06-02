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

            string uri = Url.Link("DefaultApi", new { id = pessoa.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutPessoa(int id, Pessoa pessoa)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, pessoa);

            pessoa.Id = id;
            if (!repositorio.Update(pessoa))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string uri = Url.Link("DefaultApi", new { id = pessoa.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void DeletePessoa(int id)
        {
            Pessoa pessoa = repositorio.Get(id);

            if (pessoa == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repositorio.Remove(id);
        }
    }
}
