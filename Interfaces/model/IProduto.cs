using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProduct
    {
        int? Sku { get; set; }
        double Valor { get; set; }
        double Iva { get; set; }
        string Descricao { get; set; }
    }
}
