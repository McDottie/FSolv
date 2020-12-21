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
using FSolv;
using FSolv.model;
using System.Data;
using System.Collections.Generic;

namespace FSolv.mapper.concrete
{
    class FaturaProxy: Fatura
    {
        private IContext context;
        public FaturaProxy(Fatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.Name = c.Name;
            base.EnrolledStudents = null;        
        }
        public override List<Item> EnrolledStudents
        {
            get
            {
                if (base.EnrolledStudents == null)//lazy load
                {
                    FaturaMapper cm = new FaturaMapper(context);
                    base.EnrolledStudents=cm.LoadStudents(this);
                }
                return base.EnrolledStudents;
            }

            set
            {
                base.EnrolledStudents = value;
            }
        }

    }
}
