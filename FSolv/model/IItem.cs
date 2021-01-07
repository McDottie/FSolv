using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.model
{
    public interface IItem
    {
        int? Id { get; set; }
        double Desconto { get; set; }
        int Qnt { get; set; }
        Product Product { get; set; }
        Fatura Fatura { get; set; }
        NotaCredito NotaCredito { get; set; }
    }
}
