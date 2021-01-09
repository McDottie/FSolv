using FSolv.model;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSolv.model
{
    public class NotaCredito : INotaCredito
    {
        public int? Id { get; set; }
        public virtual IFatura Fatura { get; set; }

        public DateTime DataEmissao { get; set; }

        public string State { get; set; }

        public virtual List<IItem> Items { get; set; }

    }
}
