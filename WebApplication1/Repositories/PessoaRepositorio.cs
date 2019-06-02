using System;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Repositories.Contracts;

namespace WebApplication1.Repositories
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private List<Pessoa> pessoas = new List<Pessoa>();
        private int _nextId = 1;

        public PessoaRepositorio()
        {
            Add(new Pessoa { Nome = "Ricardo Goulart", CPF = "333.444.555-66", UF = "GO", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456"});
            Add(new Pessoa { Nome = "Alberto de Nobrega", CPF = "111.222.333-64", UF = "SP", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456"});
            Add(new Pessoa { Nome = "Celio Rodrigues", CPF = "123.450.124-11", UF = "CE", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456" });
            Add(new Pessoa { Nome = "Amanda Ribeiro", CPF = "845.444.555-11", UF = "TO", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456" });
            Add(new Pessoa { Nome = "Raul da Mata", CPF = "375.333.378-67", UF = "AM", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456" });
            Add(new Pessoa { Nome = "Janaina da Silva", CPF = "567.342.633-66", UF = "SC", DataNascimento = new DateTime(2000, 3, 1), Senha = "123456" });
        }

        public Pessoa Add(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException("pessoa");
            }
            pessoa.Id = _nextId++;
            pessoas.Add(pessoa);
            return pessoa;
        }

        public Pessoa Get(int id)
        {
            return pessoas.Find(p => p.Id == id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return pessoas;
        }

        public void Remove(int id)
        {
            pessoas.RemoveAll(p => p.Id == id);
        }

        public bool Update(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException("pessoa");
            }

            int index = pessoas.FindIndex(p => p.Id == pessoa.Id);

            if (index == -1)
            {
                return false;
            }
            pessoas.RemoveAt(index);
            pessoas.Add(pessoa);
            return true;
        }
    }
}
