using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    public class ContribuinteRepository : IContribuinteRepository
    {
        private masterEntities entitys;

        public ContribuinteRepository(masterEntities entitys)
        {
            this.entitys = entitys;
        }
        public void Add(IContribuinte entity)
        {
            entitys.Contribuintes.Add((Contribuinte)entity);
        }

        public void Delete(IContribuinte entity)
        {
            entitys.Contribuintes.Remove((Contribuinte)entity);
        }

        public IEnumerable<IContribuinte> Find(Func<IContribuinte, bool> criteria)
        {
            return entitys.Contribuintes.Where(criteria);
        }

        public IEnumerable<IContribuinte> FindAll()
        {
            return entitys.Contribuintes.ToList();
        }

        public void Update(IContribuinte entity)
        {
            IContribuinte cb = entitys.Contribuintes.Where(c => c.Nif == entity.Nif).SingleOrDefault();
            cb.Name = entity.Name;
            cb.Morada = entity.Morada;
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
    }
}
