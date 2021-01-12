using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class Contribuinte : IContribuinte
    {
        public long? Nif { get => this.nif; set => this.nif = (long)value; }
        public string Name { get => this.nome; set => this.nome = value; }
        public string Morada { get => this.morada; set => this.morada = value; }
        List<IFatura> IContribuinte.Faturas { get => this.Faturas.ToList<IFatura>(); set => throw new NotImplementedException(); }
    }
}
