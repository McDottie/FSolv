using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.model
{
    public interface IProduto
    {
        int? Sku { get; set; }
        double Valor { get; set; }
        double Iva { get; set; }
        string Descricao { get; set; }
    }
}
