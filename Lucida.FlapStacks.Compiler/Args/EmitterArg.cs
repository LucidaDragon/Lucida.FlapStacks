using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class EmitterArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "emitter", "emit", "e" };

		public override string ParameterFormat => "<emitter name>";

		public override string HelpText => "Write out using the specified emitter.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length != 1) return false;

			var arg = args[0];
			configuration.OnPreCompile.Add(() =>
			{
				if (configuration.TargetPlatform != null)
				{
					foreach (var emitter in configuration.TargetPlatform.Emitters)
					{
						if (emitter.Name == arg)
						{
							configuration.TargetEmitter = emitter;
							return;
						}
					}

					throw new Exception($"Emitter \"{arg}\" is not defined.");
				}
			});

			return true;
		}
	}
}
