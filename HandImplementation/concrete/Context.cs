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
using FSolv.IndentityMap;

namespace FSolv.concrete
{
    public class Context: IContext
    {
        private string _connectionString;
        private SqlConnection con = null;

        private ContribuinteRepository _contribuinteRepository;
        private FaturaRepository _faturaRepository;
        private ItemRepository _itemRepository;
        private NotaCreditoRepository _notaCreditoRepository;
        private ProductRepository _productRepository;
        public IObjectPool Registry { get; }
        public Context(string cs)
        {
            _connectionString = cs;
            _contribuinteRepository = new ContribuinteRepository(this);
            _faturaRepository       = new FaturaRepository(this);
            _itemRepository         = new ItemRepository(this);
            _notaCreditoRepository  = new NotaCreditoRepository(this);
            _productRepository      = new ProductRepository(this);
            Registry = new ObjectPool();
        }

        public SqlConnection Connection
        {
            get
            {
                if (con == null)
                {
                    con = new SqlConnection(_connectionString);

                }
                if (con.State != ConnectionState.Open)
                    con.Open();

                EnlistTransaction();
                return con;
            }
        }
            
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }

        }

        private void EnlistTransaction()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
            }
        }

        public IContribuinteRepository Contribuinte
        {
            get
            {
                return _contribuinteRepository;
            }
        }

        public IFaturaRepository Fatura
        {
            get
            {
                return _faturaRepository;
            }
        }

        public IItemRepository Item
        {
            get
            {
                return _itemRepository;
            }
        }

        public INotaCreditoRepository NotaCredito
        {
            get
            {
                return _notaCreditoRepository;
            }
        }

        public IProductRepository Produto
        {
            get
            {
                return _productRepository;
            }
        }

    }
}
