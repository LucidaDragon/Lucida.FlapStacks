using Lucida.FlapStacks.Plugins;

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
				configuration.Plugins.Add(Importer.ImportDll(args[i]));
			}

			return true;
		}
	}
}
