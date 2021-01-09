using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface INotaCredito
    {
        string Id { get; set; }
        IFatura Fatura { get; set; }
        string State { get; set; }

        List<IItem> Items { get; set; }
    }
}
