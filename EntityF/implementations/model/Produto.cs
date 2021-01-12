using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class Produto : IProduto
    {
        public int? Sku { get => this.sku; set => this.sku = (int)value; }
        public Nullable<decimal> Valor { get => this.valor; set => this.valor = value; }
        public Nullable<decimal> Iva { get => this.iva; set => this.iva = value; }
        public string Descricao { get => this.descricao; set => this.descricao = value; }
    }
}
