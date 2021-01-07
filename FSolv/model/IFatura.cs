using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.model
{
    public interface IFatura
    {
        string Id { get; set; }
        DateTime DataEmissao { get; set; }

        string State { get; set; }

        Contribuinte Contribuinte { get; set; }

        double Iva { get; set; }

        double Total { get; set; }

        List<Item> Items { get; set; }
    }
}
