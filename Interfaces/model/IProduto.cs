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
        decimal Valor { get; set; }
        decimal Iva { get; set; }
        string Descricao { get; set; }
    }
}
