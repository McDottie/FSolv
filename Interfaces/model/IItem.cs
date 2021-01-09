using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IItem
    {
        int? Id { get; set; }
        decimal Desconto { get; set; }
        int Qnt { get; set; }
        IProduto Produto { get; set; }
        IFatura Fatura { get; set; }
        INotaCredito NotaCredito { get; set; }
    }
}
