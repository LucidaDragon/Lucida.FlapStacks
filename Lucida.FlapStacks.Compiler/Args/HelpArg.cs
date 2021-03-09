using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class HelpArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "help", "h", "?" };

		public override string ParameterFormat => "";

		public override string HelpText => "List the available flags.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			for (int i = 0; i < Arguments.Handlers.Length; i++)
			{
				var arg = Arguments.Handlers[i];

				Console.WriteLine();
				Console.WriteLine($"-{string.Join(", -", arg.ArgNames)}");
				Console.WriteLine($"Usage: -{arg.ArgNames[arg.ArgNames.Length - 1]} {arg.ParameterFormat}");
				Console.WriteLine(arg.HelpText);
			}

			Console.WriteLine();

			return true;
		}
	}
}
