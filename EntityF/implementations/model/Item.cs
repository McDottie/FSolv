﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class Item : IItem
    {
        public int? Id { get => this.id; set => this.id = (int)value; }
        public Nullable<decimal> Desconto { get => this.desconto; set => this.desconto = desconto; }
        public Nullable<int> Qnt { get => this.quantidade; set => this.quantidade = quantidade; }
        public IProduto Produto { get => this.Produto; set => throw new NotImplementedException(); }
        public IFatura Fatura { get => this.Fatura; set => throw new NotImplementedException(); }
        public INotaCredito NotaCredito { get => this.NotaCredito; set => throw new NotImplementedException(); }
    }
}
