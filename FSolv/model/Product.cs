using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSolv.model
{
    public class Product : IProduct
    {
        public int? Sku { get; set; }
        public double Valor { get; set; }
        public double Iva { get; set; }
        public string Descricao { get; set; }
    }
}
