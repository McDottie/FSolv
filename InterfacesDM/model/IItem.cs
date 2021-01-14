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
        Nullable<decimal> Desconto { get; set; }
        Nullable<int> Qnt { get; set; }
        IProduto ProdutoI { get; set; }
        IFatura Fatura { get; set; }
        INotaCredito NotaCredito { get; set; }
    }
}
