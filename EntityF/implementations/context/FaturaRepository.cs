using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    class FaturaRepository : IFaturaRepository
    {
        private masterEntities entitys;
        public FaturaRepository(masterEntities entitys)
        {
            this.entitys = entitys;
        }
        public void Add(IFatura entity)
        {
            ObjectParameter objP = new ObjectParameter("c_id", typeof(string));
            entitys.p_criaFactura(entity.Contribuinte.Nif,objP);
            entity.Id = (string)objP.Value;
        }

        public void AddItemToFatura(IFatura fatura, IItem item)
        {
            ObjectParameter objP = new ObjectParameter("id", typeof(int));
            entitys.addItem_Fatura(fatura.Id,item.ProdutoI.Sku,item.Qnt,item.Desconto, objP);
        }

        public void Delete(IFatura entity)
        {
            entitys.Faturas.Remove((Fatura)entity);
        }

        public IEnumerable<IFatura> Find(Func<IFatura, bool> criteria)
        {
            return entitys.Faturas.Where(criteria);
        }

        public IEnumerable<IFatura> FindAll()
        {
            return entitys.Faturas.ToList();
        }

        public bool Save()
        {
            try { entitys.SaveChanges(); }
            catch (DbUpdateConcurrencyException e)
            {
                e.Entries.Single().Reload();
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public void Update(IFatura entity)
        {
            Fatura ftc = entitys.Faturas.Where(fc => fc.id == entity.Id).SingleOrDefault();
            ftc.estado = entity.State;
        }
    }
}
