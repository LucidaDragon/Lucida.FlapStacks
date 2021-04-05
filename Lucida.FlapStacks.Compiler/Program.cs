using System;

namespace Lucida.FlapStacks.Compiler
{
	class Program
	{
		private readonly static Configuration Configuration = new Configuration();

		static void Main(string[] args)
		{
			try
			{
				if (!ConfigureArguments(args))
				{
					Environment.Exit(1);
					return;
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				Environment.Exit(1);
				return;
			}

			Environment.Exit(Configuration.Execute());
			return;
		}

		private static bool ConfigureArguments(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];

				if (arg.StartsWith('-') && arg.Length > 1)
				{
					arg = arg[1..];
					var subArgs = GetSubArgs(ref i, args);
					var handled = false;

					foreach (var handler in Arguments.Handlers)
					{
						if (handler.ShouldHandle(arg))
						{
							handled = handler.Handle(Configuration, subArgs);
							if (handled) break;
						}
					}

					if (handled) continue;
				}

				Console.Error.WriteLine($"Invalid argument \"{arg}\"");
				return false;
			}

			return true;
		}

		private static string[] GetSubArgs(ref int index, string[] args)
		{
			var argList = new List<string>();

			for (int j = index + 1; j < args.Length; j++)
			{
				if (args[j].StartsWith('-'))
				{
					index = j - 1;
					break;
				}

				if (j + 1 == args.Length)
				{
					index = j;
				}

				argList.Add(args[j]);
			}

			return argList.ToArray();
		}
	}
}
