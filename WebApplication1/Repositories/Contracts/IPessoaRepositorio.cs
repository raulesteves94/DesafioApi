using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories.Contracts
{
    public interface IPessoaRepositorio
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa Get(int id);
        Pessoa Add(Pessoa item);
        void Remove(int id);
        bool Update(Pessoa item);
    }
}
