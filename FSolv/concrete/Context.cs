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

        private ContribuinteRepository _contribuinteRepository;
        private FaturaRepository _faturaRepository;
        private ItemRepository _itemRepository;
        private NotaCreditoRepository _notaCreditoRepository;
        private ProductRepository _productRepository;




        public Context(string cs)
        {
            connectionString = cs;
            _contribuinteRepository = new ContribuinteRepository(this);
            _faturaRepository = new FaturaRepository(this);
            _itemRepository = new ItemRepository(this);
            _notaCreditoRepository = new NotaCreditoRepository(this);
            _productRepository = new ProductRepository(this);

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

        void IContext.Open()
        {
            throw new NotImplementedException();
        }

        SqlCommand IContext.createCommand()
        {
            throw new NotImplementedException();
        }

        void IContext.EnlistTransaction()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        public ContribuinteRepository Countribuinte
        {
            get
            {
                return _contribuinteRepository;
            }
        }

        public FaturaRepository Fatura
        {
            get
            {
                return _faturaRepository;
            }
        }

        public ItemRepository Item
        {
            get
            {
                return _itemRepository;
            }
        }

        public NotaCreditoRepository NotaCredito
        {
            get
            {
                return _notaCreditoRepository;
            }
        }

        public ProductRepository Produto
        {
            get
            {
                return _productRepository;
            }
        }

        ContribuinteRepository IContext.Contribuinte => Countribuinte;

        FaturaRepository IContext.Fatura => Fatura;

        ItemRepository IContext.Item => Item;

        NotaCreditoRepository IContext.NotaCredito => NotaCredito;

        ProductRepository IContext.Produto => Produto;
    }
}
