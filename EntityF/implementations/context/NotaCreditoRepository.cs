using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
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
            entitys.Nota_Credito.Add((Nota_Credito) entity);
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

        public void Save()
        {
            entitys.SaveChanges();
        }

        public void Update(INotaCredito entity)
        {
            INotaCredito nc = entitys.Nota_Credito.Where(n => n.Id == entity.Id).SingleOrDefault();
            nc.State = entity.State;
            nc.Items = entity.Items;
        }
    }
}
