using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    public class NotaCreditoRepository : INotaCreditoRepository
    {
        private masterEntities entitys;

        public NotaCreditoRepository(masterEntities entitys) 
        {
            this.entitys = entitys;
        }
        public void Add(INotaCredito entity)
        {
            ObjectParameter objP = new ObjectParameter("id", typeof(string));
            entitys.criaNotaCredito(entity.Fatura.Id, objP);

            entity.Id = (string)objP.Value;
        }

        public void AddItemToNC(INotaCredito nc, IItem item)
        {
            entitys.addItem_NC(nc.Id, item.Id, item.Qnt);
        }

        public void Delete(INotaCredito entity)
        {
            entitys.Nota_Credito.Remove((Nota_Credito) entity);
        }

        public IEnumerable<INotaCredito> Find(Func<INotaCredito, bool> criteria)
        {
            return entitys.Nota_Credito.Where(criteria);
        }

        public IEnumerable<INotaCredito> FindAll()
        {
            return entitys.Nota_Credito.ToList();
        }

        public IEnumerable<INotaCredito> ListNCFromYear(DateTime dateTime)
        {
            return entitys.ListNC(dateTime).ToList();
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

        public void Update(INotaCredito entity)
        {
            INotaCredito nc = entitys.Nota_Credito.Where(n => n.Id == entity.Id).SingleOrDefault();
            nc.State = entity.State;
        }
    }
}
