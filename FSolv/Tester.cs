using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FSolv.App;

namespace FSolv
{
    class Tester
    {
		private App app;
		public Tester(App app)
        {
			this.app = app;
		}

        private void EmitirFaturaAdicionandoItems()
        {
			using (IContext ctx = (IContext)Activator.CreateInstance(app._contextType, app.connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;
				IFatura newFatura = (IFatura)Activator.CreateInstance(app.modelTypeHolder[typeof(IFatura)]);

				newFatura.Contribuinte = (IContribuinte)Activator.CreateInstance(app.modelTypeHolder[typeof(IContribuinte)]);
				newFatura.Contribuinte.Nif = 12345678;
				crepo.Add(newFatura);
				crepo.Save();

				IItem item = (IItem)Activator.CreateInstance(app.modelTypeHolder[typeof(IItem)]);

				item.ProdutoI = (IProduto)Activator.CreateInstance(app.modelTypeHolder[typeof(IProduto)]);
				item.ProdutoI.Sku = 3682;
				item.Qnt = 2;
				item.Desconto = 0;

				crepo.AddItemToFatura(newFatura, item);
				crepo.Save();

				newFatura.State = "Emitida";
				crepo.Update(newFatura);
				crepo.Save();
			}
		}

        public void TestEfficiency()
		{

			long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			for (int i = 0; i < 100; i++)
			{
				EmitirFaturaAdicionandoItems();
			}
			long curr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			float value = curr - milliseconds;
			value /= 100.0f; 
			Console.WriteLine("Average Time for implementation " + app._contextType.Name + " : " + value + "ms");
		}
	}
}
