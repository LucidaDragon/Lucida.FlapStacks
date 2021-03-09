namespace Lucida.FlapStacks.Compiler.Args
{
	public class OutputArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "output", "out", "o" };

		public override string ParameterFormat => "<file>";

		public override string HelpText => "Write out the specified output file. This flag is required.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length != 1) return false;

			var arg = args[0];
			configuration.OnLoad.Add(() =>
			{
				configuration.Target = new FileStream(arg, true, true);
			});

			return true;
		}
	}
}