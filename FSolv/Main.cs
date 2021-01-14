
using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace FSolv
{


	class App
	{
		public enum Option
		{
			Unknown = -1,
			Exit,
			List_Contribuinte,
			List_Fatura,
			List_Nota_de_Credito,
			List_Produtos,
			List_Nota_de_Credito_de_Determinado_Ano,
			Register_Fatura,
			Register_Nota_de_Credito,
			Enrol_Contribuinte,
			Add_Item_to_Fatura,
			Add_Item_to_Nota_de_Credito,
			Update_Estado_da_Fatura,
			Emitir_Fatura_Adicionando_Items,

			Test_Efficiency
		}

		private static App __instance;
		public Type _contextType;
		public Dictionary<Type, Type> modelTypeHolder;
		private Dictionary<Type, IEnumerable<PropertyInfo>> propertyInfoCache;

		public string connectionString;
		private App()
		{

			__dbMethods = new Dictionary<Option, DBMethod>();
            __dbMethods.Add(Option.List_Contribuinte, ListContribuinte);
			__dbMethods.Add(Option.List_Fatura, ListFatura);
			__dbMethods.Add(Option.List_Nota_de_Credito, ListNC);
			__dbMethods.Add(Option.List_Produtos, ListProdutos);
			__dbMethods.Add(Option.List_Nota_de_Credito_de_Determinado_Ano, listNCByYear);
			__dbMethods.Add(Option.Register_Fatura, Registerfatura);
			__dbMethods.Add(Option.Register_Nota_de_Credito, RegisterNC);
			__dbMethods.Add(Option.Enrol_Contribuinte, RegisterContribuinte);
			__dbMethods.Add(Option.Add_Item_to_Fatura, AddItemToFatura);
			__dbMethods.Add(Option.Add_Item_to_Nota_de_Credito, AddItemToNC);
			__dbMethods.Add(Option.Update_Estado_da_Fatura, UpdateStateFatura);
			__dbMethods.Add(Option.Emitir_Fatura_Adicionando_Items, EmitirAddCriarFatura);
			__dbMethods.Add(Option.Test_Efficiency, TestEfficiency);

			modelTypeHolder = new Dictionary<Type, Type>();
			propertyInfoCache = new Dictionary<Type, IEnumerable<PropertyInfo>>();

			connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;
			string path = ConfigurationManager.AppSettings["AssemblyPath"];

			Assembly asm = Assembly.LoadFrom(path);
			foreach (Type t in asm.GetExportedTypes())
			{
				if (typeof(IContext).IsAssignableFrom(t))
					_contextType = t;

				Type[] itfs = t.GetInterfaces();
				if(itfs.Length > 0 && !modelTypeHolder.ContainsKey(itfs[0]))
					modelTypeHolder.Add(itfs[0], t);
			}

		}

        private void TestEfficiency()
        {
			Tester tester = new Tester(__instance);
			tester.TestEfficiency();

		}

        public static App Instance
		{
			get
			{
				if (__instance == null)
					__instance = new App();
				return __instance;
			}
			private set { }
		}

		private Option DisplayMenu()
		{
			Option option = Option.Unknown;
			try
			{
				Console.WriteLine("FSolv management");
				Console.WriteLine();
				int i = 0;
				foreach (Option opt in Enum.GetValues(typeof(Option)))
				{
					if (opt != option)
					{
						Console.WriteLine(i++ + ". " + opt.ToString().Replace('_', ' '));
					}
				}

				string result = Console.ReadLine();

				option = (Option)Enum.Parse(typeof(Option), result);
			}
			catch (ArgumentException ex)
			{
				//nothing to do. User select unknown option and press enter.
			}

			return option;

		}

		public delegate void DBMethod();
		public System.Collections.Generic.Dictionary<Option, DBMethod> __dbMethods;
		public string ConnectionString
		{
			get;
			set;
		}

		public void Run()
		{
			Option userInput = Option.Unknown;
			do
			{
				Console.Clear();
				userInput = DisplayMenu();
				Console.Clear();
				try
				{
					__dbMethods[userInput]();
					Console.ReadKey();
				}
				catch (KeyNotFoundException ex)
				{
					//Nothing to do. The option was not a valid one. Read another.
				}

			} while (userInput != Option.Exit);
		}

		private void printResults<T>(IEnumerator<T> results)
		{
			IEnumerable<PropertyInfo> properties;

			if (propertyInfoCache.ContainsKey(typeof(T)))
            {
				properties = propertyInfoCache[typeof(T)];
			}
			else 
			{
				properties = typeof(T).GetProperties().Where(x =>
				 {
					 Type type;
					 type = x.PropertyType;

					return !type.FullName.Contains("Interfaces");

				});
				propertyInfoCache.Add(typeof(T),properties);
			}


			int size = properties.Count();

			int[] formatSizes = new int[size];
			List<string> attrib;
			List<string> values;
			string separator;
			GetFromatSizesStrings(out attrib, out values, out separator, out formatSizes, properties, results);
			int fcnt = 0;

			Console.Write(separator);
			Console.WriteLine();
			Console.Write('|');
			foreach (string pi in attrib)
			{
				Console.Write(String.Format("{0,-" + formatSizes[fcnt] + "}",
					String.Format("{0," + ((formatSizes[fcnt] + pi.Length) / 2).ToString() + "}", pi) ) 
					+ "|");
				fcnt++;
			}
			Console.WriteLine();
			Console.Write(separator);
			Console.WriteLine();
			IEnumerator<string> vls = values.GetEnumerator();
			bool canMove = vls.MoveNext();
			while (canMove) {

				string curr = vls.Current;

				Console.Write('|');
				for (int i = 0; i < size; i++)
				{
					Console.Write(String.Format("{0,-" + formatSizes[i] + "}", curr) + "|");
					canMove = vls.MoveNext();
					curr = vls.Current;
				}
				Console.WriteLine();
			}

			Console.Write(separator);
			Console.WriteLine();
		}

        private static void GetFromatSizesStrings<T>(out List<string> attrib,
													out List<string> values,
													out string separator,
													out int[] formatSizes, 
													IEnumerable<PropertyInfo> properties,
													IEnumerator<T> results)
        {
			int size = properties.Count();
			formatSizes = new int[size];
			attrib = new List<string>();
			values = new List<string>();
			separator =  "+";
			
			int cnt = 0;

			foreach (PropertyInfo pi in properties)
			{
				attrib.Add(pi.Name);
				formatSizes[cnt++] = pi.Name.Length;
			}


			while (results.MoveNext())
			{
				T curr = results.Current;
				int ict = 0;
				foreach (PropertyInfo pi2 in properties)
				{
					object value = pi2.GetValue(curr);
					string v = value == null ? "" : value.ToString();

					if (formatSizes[ict] < v.Length)
						formatSizes[ict] = v.Length;
					values.Add(v);
					ict++;
				}
			}


			for (int i = 0; i < size; ++i)
			{
				separator += new String('-', formatSizes[i]);
				separator += '+';
			}
		}

        private void ListFatura()
		{
			Console.WriteLine("Faturas");
			Console.WriteLine();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;

				printResults<IFatura>(crepo.FindAll().GetEnumerator());
			}
		}
		private void ListContribuinte()
        {
			Console.WriteLine("Contribuintes");
			Console.WriteLine();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType,connectionString))
			{
				IContribuinteRepository crepo = ctx.Contribuinte;

				printResults<IContribuinte>(crepo.FindAll().GetEnumerator());
			}
		}

		private void ListNC()
		{
			Console.WriteLine("Notas de Credito");
			Console.WriteLine();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				INotaCreditoRepository crepo = ctx.NotaCredito;

				printResults<INotaCredito>(crepo.FindAll().GetEnumerator());
			}
		}

		private void ListProdutos()
		{
			Console.WriteLine("Produtos");
			Console.WriteLine();
			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IProductRepository crepo = ctx.Produto;

				printResults<IProduto>(crepo.FindAll().GetEnumerator());
			}
		}

		private void listNCByYear()
        {

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				INotaCreditoRepository crepo = ctx.NotaCredito;
				List<string> questions = new List<string>();
				questions.Add("Que ano pretende ver");

				string[] inputs = QuestionRoutine(questions).ToArray();

				printResults<INotaCredito>(crepo.ListNCFromYear(new DateTime(Convert.ToInt32(inputs[0]),1,1)).GetEnumerator());
			}
		}

		private void RegisterContribuinte()
		{
			
			List<string> questions = new List<string>();
			questions.Add("Coloque o Nif do Contribuinte");
			questions.Add("Coloque o Nome completo do contribuinte");
			questions.Add("Coloque a sua morada (dê ENTER se não pretender adicionar uma)");

			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IContribuinteRepository crepo = ctx.Contribuinte;
				IContribuinte newContr = (IContribuinte)Activator.CreateInstance(modelTypeHolder[typeof(IContribuinte)]);

				newContr.Nif = long.Parse(inputs[0]);
				newContr.Name = inputs[1];
				newContr.Morada = inputs[2];

				crepo.Add(newContr);
				crepo.Save();
				printResults<IContribuinte>(crepo.FindAll().GetEnumerator());

			}
		}
        private void Registerfatura()
		{
			List<string> questions = new List<string>();
			questions.Add("Coloque o Nif do Contribuinte desta Fatura?");
			
			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;
				IFatura newFatura = (IFatura)Activator.CreateInstance(modelTypeHolder[typeof(IFatura)]);

				newFatura.Contribuinte = (IContribuinte)Activator.CreateInstance(modelTypeHolder[typeof(IContribuinte)]);
				newFatura.Contribuinte.Nif = long.Parse(inputs[0]);
				crepo.Add(newFatura);
				crepo.Save();
				printResults<IFatura>(crepo.FindAll().GetEnumerator());

			}
		}

		private void RegisterNC()
        {
			List<string> questions = new List<string>();
			questions.Add("Coloque o Id da Fatura correspondente?");

			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				INotaCreditoRepository crepo = ctx.NotaCredito;
				INotaCredito nc = (INotaCredito)Activator.CreateInstance(modelTypeHolder[typeof(INotaCredito)]);

				nc.Fatura = (IFatura)Activator.CreateInstance(modelTypeHolder[typeof(IFatura)]);
				nc.Fatura.Id = inputs[0];
				crepo.Add(nc);
				crepo.Save();
				printResults<INotaCredito>(crepo.FindAll().GetEnumerator());

			}
		}

		private void AddItemToFatura()
        {
			List<string> questions = new List<string>();
			questions.Add("Qual o id da Fatura?");
			questions.Add("Qual o Produto que pretende adicionar à fatura (SKU)?");
			questions.Add("Qual a quantidade do produto?");
			questions.Add("Qual o desconto do item?");

			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;
				IFatura fatura = (IFatura)Activator.CreateInstance(modelTypeHolder[typeof(IFatura)]);
				IItem item = (IItem)Activator.CreateInstance(modelTypeHolder[typeof(IItem)]);
				
				fatura.Id = inputs[0];
				
				item.ProdutoI = (IProduto)Activator.CreateInstance(modelTypeHolder[typeof(IProduto)]);
				item.ProdutoI.Sku = int.Parse(inputs[1]);
				item.Qnt = Convert.ToInt32(inputs[2]);
				item.Desconto = Convert.ToDecimal(inputs[3]);

				crepo.AddItemToFatura(fatura,item);
				crepo.Save();
				printResults<IFatura>(crepo.FindAll().GetEnumerator());

			}
		}

		private void AddItemToNC()
		{
			List<string> questions = new List<string>();
			questions.Add("Qual o id da Nota de Credito?");
			questions.Add("Qual o Item que pretende remover da fatura (id do item)?");
			questions.Add("Qual a quantidade que pretende retirar?");

			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				INotaCreditoRepository crepo = ctx.NotaCredito;
				INotaCredito nc = (INotaCredito)Activator.CreateInstance(modelTypeHolder[typeof(INotaCredito)]);
				IItem item = (IItem)Activator.CreateInstance(modelTypeHolder[typeof(IItem)]);

				nc.Id = inputs[0];
				item.Id = Convert.ToInt32(inputs[1]);
				item.Qnt = Convert.ToInt32(inputs[2]);
				
				crepo.AddItemToNC(nc, item);
				crepo.Save();
				printResults<INotaCredito>(crepo.FindAll().GetEnumerator());

			}
		}

        private void UpdateStateFatura()
        {

			List<string> questions = new List<string>();
			questions.Add("Qual o id da Fatura?");
			questions.Add("Para que estado é que quere alterar esta Fatura?");

			string[] inputs = QuestionRoutine(questions).ToArray();

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;

				IFatura fatura = (IFatura)Activator.CreateInstance(modelTypeHolder[typeof(IFatura)]); ;
				fatura.Id = inputs[0];
				fatura.State = inputs[1];
				crepo.Update(fatura);
				crepo.Save();
				printResults<IFatura>(crepo.FindAll().GetEnumerator());
			}
		}

		private void EmitirAddCriarFatura()
        {
			List<string> questions = new List<string>();
			questions.Add("Coloque o Nif do Contribuinte desta Fatura?");
			questions.Add("Qual o Produto que pretende adicionar à fatura (SKU)?");
			questions.Add("Qual a quantidade do produto?");
			questions.Add("Qual o desconto do item?");

			string[] inputs = QuestionRoutine(questions).ToArray();
			using(var ts = new TransactionScope())
			{
				using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
				{
					IFaturaRepository crepo = ctx.Fatura;
					IFatura newFatura = (IFatura)Activator.CreateInstance(modelTypeHolder[typeof(IFatura)]);

					newFatura.Contribuinte = (IContribuinte)Activator.CreateInstance(modelTypeHolder[typeof(IContribuinte)]);
					newFatura.Contribuinte.Nif = long.Parse(inputs[0]);
					crepo.Add(newFatura);
					crepo.Save();

					IItem item = (IItem)Activator.CreateInstance(modelTypeHolder[typeof(IItem)]);

					item.ProdutoI = (IProduto)Activator.CreateInstance(modelTypeHolder[typeof(IProduto)]);
					item.ProdutoI.Sku = int.Parse(inputs[1]);
					item.Qnt = Convert.ToInt32(inputs[2]);
					item.Desconto = Convert.ToDecimal(inputs[3]);

					crepo.AddItemToFatura(newFatura, item);
					crepo.Save();

					newFatura.State = "Emitida";
					crepo.Update(newFatura);
					crepo.Save();

					printResults<IFatura>(crepo.FindAll().GetEnumerator());
					ts.Complete();
				}
			}
		}

		private List<string> QuestionRoutine(List<string> questions)
        {
			List<string> result = new List<string>();
            foreach(string question in questions)
            {
				Console.WriteLine(question);
				result.Add(Console.ReadLine());
            }
			return result;
        }



		}
class MainClass
{

	public static void Main(string[] args)
	{
		App.Instance.Run();
	}

	}
}
