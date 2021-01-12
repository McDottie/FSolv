﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class masterEntities : DbContext
    {
        public masterEntities()
            : base("name=masterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alteracao_Fatura> Alteracao_Fatura { get; set; }
        public virtual DbSet<code> codes { get; set; }
        public virtual DbSet<Contribuinte> Contribuintes { get; set; }
        public virtual DbSet<Fatura> Faturas { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Nota_Credito> Nota_Credito { get; set; }
        public virtual DbSet<Produto> Produtoes { get; set; }
        public virtual DbSet<fatura_contribuinte> fatura_contribuinte { get; set; }
    
        [DbFunction("masterEntities", "ListNC")]
        public virtual IQueryable<ListNC_Result> ListNC(Nullable<System.DateTime> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ListNC_Result>("[masterEntities].[ListNC](@year)", yearParameter);
        }
    
        public virtual int addItem_Fatura(string cod_fatura, Nullable<int> sku, Nullable<int> qnt, Nullable<decimal> desconto, ObjectParameter id)
        {
            var cod_faturaParameter = cod_fatura != null ?
                new ObjectParameter("cod_fatura", cod_fatura) :
                new ObjectParameter("cod_fatura", typeof(string));
    
            var skuParameter = sku.HasValue ?
                new ObjectParameter("sku", sku) :
                new ObjectParameter("sku", typeof(int));
    
            var qntParameter = qnt.HasValue ?
                new ObjectParameter("qnt", qnt) :
                new ObjectParameter("qnt", typeof(int));
    
            var descontoParameter = desconto.HasValue ?
                new ObjectParameter("desconto", desconto) :
                new ObjectParameter("desconto", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addItem_Fatura", cod_faturaParameter, skuParameter, qntParameter, descontoParameter, id);
        }
    
        public virtual int addItem_NC(string cod_nc, Nullable<int> item, Nullable<int> qnt)
        {
            var cod_ncParameter = cod_nc != null ?
                new ObjectParameter("cod_nc", cod_nc) :
                new ObjectParameter("cod_nc", typeof(string));
    
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            var qntParameter = qnt.HasValue ?
                new ObjectParameter("qnt", qnt) :
                new ObjectParameter("qnt", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addItem_NC", cod_ncParameter, itemParameter, qntParameter);
        }
    
        public virtual int alt_estado_fatura(string id_fatura, string estado)
        {
            var id_faturaParameter = id_fatura != null ?
                new ObjectParameter("id_fatura", id_fatura) :
                new ObjectParameter("id_fatura", typeof(string));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("estado", estado) :
                new ObjectParameter("estado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("alt_estado_fatura", id_faturaParameter, estadoParameter);
        }
    
        public virtual int createNCAddItems(string id_fatura)
        {
            var id_faturaParameter = id_fatura != null ?
                new ObjectParameter("id_fatura", id_fatura) :
                new ObjectParameter("id_fatura", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("createNCAddItems", id_faturaParameter);
        }
    
        public virtual int criaNotaCredito(string id_fatura, ObjectParameter id)
        {
            var id_faturaParameter = id_fatura != null ?
                new ObjectParameter("id_fatura", id_fatura) :
                new ObjectParameter("id_fatura", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("criaNotaCredito", id_faturaParameter, id);
        }
    
        public virtual int makeID(Nullable<bool> fat_or_nc, ObjectParameter id)
        {
            var fat_or_ncParameter = fat_or_nc.HasValue ?
                new ObjectParameter("fat_or_nc", fat_or_nc) :
                new ObjectParameter("fat_or_nc", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("makeID", fat_or_ncParameter, id);
        }
    
        public virtual int p_criaFactura(Nullable<int> nif, ObjectParameter c_id)
        {
            var nifParameter = nif.HasValue ?
                new ObjectParameter("nif", nif) :
                new ObjectParameter("nif", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_criaFactura", nifParameter, c_id);
        }
    }
}
