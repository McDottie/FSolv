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
using System;
using System.Collections.Generic;
using System.Linq;

using FSolv.model;
using FSolv.mapper.concrete;
using Interfaces;

namespace FSolv.concrete
{
    public class FaturaRepository : IFaturaRepository
    {
        private IContext context;
        public FaturaRepository(IContext ctx)
        {
            context = ctx;
        }

        public void Add(IFatura entity)
        {
            new FaturaMapper(context).Create(entity);
        }

        public void Delete(IFatura entity)
        {
            new FaturaMapper(context).Delete(entity);
        }

        public IEnumerable<IFatura> Find(Func<IFatura, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IFatura> FindAll()
        {
            return new FaturaMapper(context).ReadAll();
        }

        public void Update(IFatura entity)
        {
            new FaturaMapper(context).Update(entity);
        }

        public void Save()
        {
        }

    }
}
