using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    class ProdutoRepository : IProductRepository
    {
        private masterEntities entities;

        public ProdutoRepository(masterEntities entities) 
        {
            this.entities = entities;
        }
        public void Add(IProduto entity)
        {
            entities.Produtoes.Add((Produto)entity);
        }

        public void Delete(IProduto entity)
        {
            entities.Produtoes.Remove((Produto)entity);
        }

        public IEnumerable<IProduto> Find(Func<IProduto, bool> criteria)
        {
            return entities.Produtoes.Where(criteria);
        }

        public IEnumerable<IProduto> FindAll()
        {
            return entities.Produtoes.ToList();
        }

        public void Save()
        {
            entities.Produtoes.SaveChanges();
        }

        public void Update(IProduto entity)
        {
            IProduto produto = entities.Produtoes.Where(p => p.Sku == entity.Sku).SingleOrDefault();
            produto.Valor = entity.Valor;
            produto.Descricao = entity.Descricao;
            produto.Iva = entity.Iva;
        }
    }
}
