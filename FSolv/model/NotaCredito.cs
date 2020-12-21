using FSolv.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSolv.model
{
    public class NotaCredito
    {
        public int? Id { get; set; }
        public Fatura Fatura { get; set; }

        public DateTime DataEmissao { get; set; }

        public string State { get; set; }

        public virtual List<Item> Items { get; set; }

    }
}
