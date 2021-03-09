using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class ParserArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "parser", "prs", "format", "fmt", "f" };

		public override string ParameterFormat => "<format name>";

		public override string HelpText => "Read in using the specified format.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length != 1) return false;

			var arg = args[0];
			configuration.OnPreCompile.Add(() =>
			{
				if (configuration.SourcePlatform != null)
				{
					foreach (var parser in configuration.SourcePlatform.Parsers)
					{
						if (parser.Name == arg)
						{
							configuration.SourceParser = parser;
							return;
						}
					}

					throw new Exception($"Parser \"{arg}\" is not defined.");
				}
			});

			return true;
		}
	}
}
