using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class Fatura : IFatura
    {
        public string Id { get => this.id; set => this.id = value; }
        public Nullable<DateTime> DataEmissao { get => this.dt_emissao; set => this.dt_emissao = value; }
        public string State { get => this.estado; set => this.estado = value; }
        public Nullable<decimal> Iva { get => this.iva; set => this.iva = value; }
        public Nullable<decimal> Total { get => this.valor_total; set => this.valor_total = value; }
        IContribuinte IFatura.Contribuinte { get => this.Contribuinte; set => throw new NotImplementedException(); }
        List<IItem> IFatura.Items { get => this.Items.ToList<IItem>(); set => throw new NotImplementedException(); }
    }
}
