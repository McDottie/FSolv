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
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace FSolv.concrete
{
    class Context: IContext
    {
        private string connectionString;
        private SqlConnection con = null;

        private ContribuinteRepository _countryRepository;
        private FaturaRepository _courseRepository;
        private ItemRepository _studentRepository;

      
        public Context(string cs)
        {
            connectionString = cs;
            _countryRepository = new ContribuinteRepository(this);
            _courseRepository = new FaturaRepository(this);
            _studentRepository = new ItemRepository(this);
        }
        
        public void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(connectionString);

            }
            if (con.State != ConnectionState.Open)
                con.Open();
        }

        public SqlCommand createCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
            return cmd;
        }
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }

        }

       public void EnlistTransaction()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);               
            }
        }

        public ContribuinteRepository Countries
        {
            get
            {
                return _countryRepository;
            }
        }

        public FaturaRepository Courses
        {
            get
            {
                return _courseRepository;
            }
        }

        public ItemRepository Students
        {
            get
            {
                return _studentRepository;
            }
        }


    }
}
