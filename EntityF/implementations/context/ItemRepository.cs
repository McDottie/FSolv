﻿using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    class ItemRepository : IItemRepository
    {
        private masterEntities entities;

        public ItemRepository(masterEntities entities) {
            this.entities = entities;
        }

        public void Add(IItem entity)
        {
            entities.Items.Add((Item) entity);
        }

        public void Delete(IItem entity)
        {
            entities.Items.Remove((Item) entity);
        }

        public IEnumerable<IItem> Find(Func<IItem, bool> criteria)
        {
            return entities.Items.Where(criteria);
        }

        public IEnumerable<IItem> FindAll()
        {
            return entities.Items.ToList();
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        public void Update(IItem entity)
        {
            IItem item = entities.Items.Where(i => i.Id == entity.Id).SingleOrDefault();
            item.Desconto = entity.Desconto;
            item.Qnt = entity.Qnt;
        }
    }
}
