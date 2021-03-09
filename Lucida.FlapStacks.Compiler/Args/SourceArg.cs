using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class SourceArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "source", "src", "s" };

		public override string ParameterFormat => "<platform name>";

		public override string HelpText => "Specifies the source platform. This flag is required.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length != 1) return false;

			var arg = args[0];
			configuration.OnLoad.Add(() =>
			{
				for (int i = 0; i < configuration.Plugins.Count; i++)
				{
					if (configuration.Plugins[i].Module.Platform == arg)
					{
						configuration.SourcePlatform = configuration.Plugins[i];

						if (configuration.SourcePlatform.Module.HasDefaultSource)
						{
							configuration.SourceParser = configuration.SourcePlatform.Module.DefaultSource;
						}
						else if (configuration.SourcePlatform.Parsers.Count == 1)
						{
							configuration.SourceParser = configuration.SourcePlatform.Parsers[0];
						}

						return;
					}
				}

				throw new Exception($"Platform \"{arg}\" is not defined.");
			});

			return true;
		}
	}
}
