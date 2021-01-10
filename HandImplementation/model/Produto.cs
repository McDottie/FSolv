using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSolv.model
{
    public class Produto : IProduto
    {
        public int? Sku { get; set; }
        public decimal Valor { get; set; }
        public decimal Iva { get; set; }
        public string Descricao { get; set; }
    }
}
