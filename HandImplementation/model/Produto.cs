using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSolv.model
{
    public class Produto : IProduto
    {
        public int? Sku { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public string Descricao { get; set; }
    }
}
