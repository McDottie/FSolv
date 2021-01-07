﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.model
{
    public interface INotaCredito
    {
        int? Id { get; set; }
        Fatura Fatura { get; set; }

        DateTime DataEmissao { get; set; }

        string State { get; set; }

        List<Item> Items { get; set; }
    }
}
