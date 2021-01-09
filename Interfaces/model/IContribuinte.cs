using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IContribuinte
    {
        int? Nif { get; set; }
        string Name { get; set; }
        string Morada { get; set; }
        List<IFatura> Faturas { get; set; }
    }
}
