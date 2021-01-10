
using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
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
			PropertyInfo[] properties = typeof(T).GetProperties();



			while (results.MoveNext()) {
				T curr = results.Current;

				foreach (PropertyInfo pi in properties)
                {
					Type type;
					
					type = pi.PropertyType;
                    
					if (!type.FullName.Contains("Interfaces"))
                    {
						Console.Write(pi.Name + " : " + pi.GetValue(curr));
						Console.Write("  |  ");
                    }
				}
				Console.WriteLine();
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
