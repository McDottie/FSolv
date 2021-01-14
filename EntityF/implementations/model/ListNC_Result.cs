using Interfaces;
using System;
using System.Collections.Generic;


namespace EntityF
{
    public partial class ListNC_Result : INotaCredito
    {
        public string Id { get => this.id; set => this.id = value; }
        public string State { get => this.estado; set => this.estado = value; }
        IFatura INotaCredito.Fatura { get ; set; }
        List<IItem> INotaCredito.Items { get; set; }
    }
}
