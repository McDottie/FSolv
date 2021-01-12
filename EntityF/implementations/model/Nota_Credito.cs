using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class Nota_Credito : INotaCredito
    {
        public string Id { get => this.id; set => this.id = value; }
        public string State { get => this.estado; set => this.estado = value; }
        IFatura INotaCredito.Fatura { get => this.Fatura; set => throw new NotImplementedException(); }
        List<IItem> INotaCredito.Items { get => this.Items.ToList<IItem>(); set => throw new NotImplementedException(); }
    }
}
