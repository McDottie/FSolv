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
    class ItemProxy : Item
    {
        private IContext context;
        private int? countryId;
        
        public ItemProxy(Item s, IContext ctx, int? countryId):base()
        {
            base.Number = s.Number;
            base.Name = s.Name;
            base.Sex = s.Sex;
            base.DateOfBirth = s.DateOfBirth;
            base.EnrolledCourses = null;
            base.HomeCountry = null;
            context = ctx;
            this.countryId = countryId;
        }

        public override Contribuinte HomeCountry
        {
            get
            {
                if (base.HomeCountry == null) //lazy load
                {
                    ItemMapper sm = new ItemMapper(context);
                    base.HomeCountry = sm.LoadCountry(this);
                }
                return base.HomeCountry;
            }

            set
            {
                base.HomeCountry = value;
            }
        }

        public override List<Fatura> EnrolledCourses
        {
            get
            {
                if (base.EnrolledCourses == null)
                {
                    ItemMapper sm = new ItemMapper(context);
                    base.EnrolledCourses=sm.LoadCourses(this);
                }
                return base.EnrolledCourses;
            }

            set
            {
                base.EnrolledCourses = value;
            }
        }

    }
}
