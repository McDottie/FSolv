
using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace FSolv
{


	class App
	{
		private enum Option
		{
			Unknown = -1,
			Exit,
			List_Contribuinte,
			List_Fatura,
			List_Nota_de_Credito,
			List_Produtos,
			Register_Fatura,
			Enrol_Contribuinte,
			Enrol_Produto,
			Add_Item_to_Fatura,
			Add_Item_to_Nota_de_Credito,


		}
		private static App __instance;
		private Type _contextType;
		private App()
		{
			__dbMethods = new Dictionary<Option, DBMethod>();
			__dbMethods.Add(Option.List_Contribuinte, ListContribuinte);
			__dbMethods.Add(Option.List_Fatura, ListFatura);
			__dbMethods.Add(Option.List_Nota_de_Credito, ListNC);
			__dbMethods.Add(Option.List_Produtos, ListProdutos);
			__dbMethods.Add(Option.Enrol_Contribuinte, RegisterStudent);
			__dbMethods.Add(Option.Enrol_Produto, EnrolStudent);
			__dbMethods.Add(Option.Add_Item_to_Fatura, EnrolStudent);
			__dbMethods.Add(Option.Add_Item_to_Nota_de_Credito, EnrolStudent);

			string path = ConfigurationManager.AppSettings["AssemblyPath"];

			Assembly asm = Assembly.LoadFrom(path);
			foreach (Type t in asm.GetExportedTypes())
			{
				_contextType = t;
				if (typeof(IContext).IsAssignableFrom(_contextType))
					break;
			}

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
				Console.WriteLine("Course management");
				Console.WriteLine();
				int i = 0;
				foreach(Option opt in Enum.GetValues(typeof(Option)))
                {
					if (opt != option)
						Console.WriteLine(i++ + ". " + opt.ToString().Replace('_',' '));
				}
				var result = Console.ReadLine();
				option = (Option)Enum.Parse(typeof(Option), result);
			}
			catch (ArgumentException ex)
			{
				//nothing to do. User select unknown option and press enter.
			}

			return option;

		}
		
		private delegate void DBMethod();
		private System.Collections.Generic.Dictionary<Option, DBMethod> __dbMethods;
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

		private static void printResults<T>(IEnumerator<T> results)
		{
			IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().Where(x =>
			{
				Type type;
				type = x.PropertyType;

				return !type.FullName.Contains("Interfaces");
			
			});


			int size = properties.Count();

			int[] formatSizes = new int[size];
			List<string> attrib;
			List<string> values;
			string separator;
			GetFromatSizesStrings(out attrib, out values, out separator, out formatSizes, properties, results);
			int fcnt = 0;

			Console.Write(separator);
			Console.WriteLine();

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
				
				for(int i = 0; i < size; i++)
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
			separator =  null;
			
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
			string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IFaturaRepository crepo = ctx.Fatura;

				printResults<IFatura>(crepo.FindAll().GetEnumerator());
			}
		}
		private void ListContribuinte()
        {
			string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;
			
			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType,connectionString))
			{
				IContribuinteRepository crepo = ctx.Contribuinte;

				printResults<IContribuinte>(crepo.FindAll().GetEnumerator());
			}
		}

		private void ListNC()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				INotaCreditoRepository crepo = ctx.NotaCredito;

				printResults<INotaCredito>(crepo.FindAll().GetEnumerator());
			}
		}

		private void ListProdutos()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;

			using (IContext ctx = (IContext)Activator.CreateInstance(_contextType, connectionString))
			{
				IProductRepository crepo = ctx.Produto;

				printResults<IProduto>(crepo.FindAll().GetEnumerator());
			}
		}

		private void RegisterStudent()
		{
			//TODO: Implement
			Console.WriteLine("RegisterStudent()");
		}
		private void EnrolStudent()
		{
			//TODO: Implement
			Console.WriteLine("EnrolStudent()");
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
