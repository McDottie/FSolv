/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*/
using System.Linq;
using System.Collections.Generic;
using FSolv.mapper.concrete;
using System;
using FSolv.model;
using Interfaces;

namespace FSolv.concrete
{
    public class ItemRepository : IItemRepository
    {
        private IContext context;
        public ItemRepository(IContext ctx)
        {
            context = ctx;
        }

        public void Add(IItem entity)
        {
            new ItemMapper(context).Create(entity);
        }

        public void Delete(IItem entity)
        {
            new ItemMapper(context).Delete(entity);
        }

        public IEnumerable<IItem> Find(Func<IItem, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IItem> FindAll()
        {
            return new ItemMapper(context).ReadAll();
        }

        public void Update(IItem entity)
        {
            new ItemMapper(context).Update(entity);
        }

        public void Save()
        {
        }
    }
}
