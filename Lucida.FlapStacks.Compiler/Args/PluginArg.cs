using Lucida.FlapStacks.Plugins;
using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class PluginArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "plugins", "plugin", "p" };

		public override string ParameterFormat => "<plugin file> ...";

		public override string HelpText => "Load the specified plugins.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				try
				{
					configuration.Plugins.Add(Importer.ImportDll(args[i]));
				}
				catch (Exception ex)
				{
					throw new Exception($"Failed to load plugin \"{args[i]}\": \"{ex.Message}\"");
				}
			}

			return true;
		}
	}
}
