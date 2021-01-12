using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFatura
    {
        string Id { get; set; }
        Nullable<DateTime> DataEmissao { get; set; }

        string State { get; set; }

        IContribuinte Contribuinte { get; set; }

        Nullable<decimal> Iva { get; set; }

        Nullable<decimal> Total { get; set; }

        List<IItem> Items { get; set; }
    }
}
