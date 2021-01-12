using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProduto
    {
        int? Sku { get; set; }
        Nullable<decimal> Valor { get; set; }
        Nullable<decimal> Iva { get; set; }
        string Descricao { get; set; }
    }
}
