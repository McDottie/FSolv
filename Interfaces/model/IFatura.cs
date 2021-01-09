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
        DateTime DataEmissao { get; set; }

        string State { get; set; }

        IContribuinte Contribuinte { get; set; }

        double Iva { get; set; }

        double Total { get; set; }

        List<IItem> Items { get; set; }
    }
}
