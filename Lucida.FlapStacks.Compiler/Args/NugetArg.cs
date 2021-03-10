using Lucida.FlapStacks.Plugins;
using Lucida.FlapStacks.Plugins.NuGet;
using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class NugetArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "nuget", "nu", "n" };

		public override string ParameterFormat => "<package name>@<version> ...";

		public override string HelpText => "Installs specified plugins from NuGet.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			var packages = new List<string[]>();

			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				var parts = arg.Split('@');

				if (parts.Length != 2) throw new Exception($"Invalid package and version \"{arg}\".");

				packages.Add(parts);
			}

			for (int i = 0; i < packages.Count; i++)
			{
				var package = packages[i];

				var plugins = Fetcher.Install(package[0], package[1], Console.WriteLine);

				for (int j = 0; j < plugins.Length; j++)
				{
					configuration.Plugins.Add(Importer.ImportDll(plugins[j]));
				}
			}

			return true;
		}
	}
}
