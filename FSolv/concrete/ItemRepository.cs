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
using FSolvlv.mapper.concrete;
using System;
using FSolv.model;

namespace FSolv.concrete
{
    public class ItemRepository : IItemRepository
    {
        private IContext context;
        public ItemRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Item> Find(Func<Item, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Item> FindAll()
        {
            return new ItemMapper(context).ReadAll();
        }

    }
}
