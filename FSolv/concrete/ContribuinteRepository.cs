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
using FSolv.model;
using FSolv.mapper.concrete;

namespace FSolv.concrete
{
    
    public class ContribuinteRepository : IContribuinteRepository
    {
        private IContext context;
        public ContribuinteRepository(IContext ctx)
        {
            context = ctx;
        }
         
        public IEnumerable<Contribuinte> Find(System.Func<Contribuinte, bool> criteria)
        {
            //Implementação muito pouco eficiente!!!!  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Contribuinte> FindAll()
        {
            return new ContribuinteMapper(context).ReadAll();
        }

    }
}
