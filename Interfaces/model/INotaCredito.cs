using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface INotaCredito
    {
        int? Id { get; set; }
        IFatura Fatura { get; set; }

        DateTime DataEmissao { get; set; }

        string State { get; set; }

        List<IItem> Items { get; set; }
    }
}
