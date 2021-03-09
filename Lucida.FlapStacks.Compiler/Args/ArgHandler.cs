namespace Lucida.FlapStacks.Compiler.Args
{
	public abstract class ArgHandler
	{
		public abstract string[] ArgNames { get; }

		public abstract string ParameterFormat { get; }
		public abstract string HelpText { get; }

		public bool ShouldHandle(string arg)
		{
			for (int i = 0; i < ArgNames.Length; i++)
			{
				if (arg == ArgNames[i]) return true;
			}

			return false;
		}

		public abstract bool Handle(Configuration configuration, string[] args);
	}
}
