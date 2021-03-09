namespace Lucida.FlapStacks.Compiler.Args
{
	public class IncludeArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "include", "inc", "i" };

		public override string ParameterFormat => "<file> ...";

		public override string HelpText => "Read in the specified source files. This flag is required.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			configuration.OnLoad.Add(() =>
			{
				var streams = new List<Stream>();

				if (configuration.Source != null)
				{
					streams.Add(configuration.Source);
				}

				for (int i = 0; i < args.Length; i++)
				{
					streams.Add(new FileStream(args[i], false, false));
				}

				configuration.Source = new MultiStream(streams);
			});

			return true;
		}
	}
}