using System;
using System.Diagnostics;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class PreArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "pre", "prebuild" };

		public override string ParameterFormat => "<command> <args>";

		public override string HelpText => "Execute a command before compiling.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length < 1 || args.Length > 2) return false;

			var command = args[0];
			var arguments = args.Length == 1 ? string.Empty : args[1];

			configuration.OnLoad.Add(() =>
			{
				var process = Process.Start(command, arguments);
				var name = process.ProcessName;
				process.WaitForExit();

				if (process.ExitCode != 0) throw new Exception($"Process exited with code {process.ExitCode}: {name}");
			});

			return true;
		}
	}
}
