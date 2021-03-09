using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class TargetArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "target", "tar", "t" };

		public override string ParameterFormat => "<platform name>";

		public override string HelpText => "Specifies the target platform. This flag is required.";

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
						configuration.TargetPlatform = configuration.Plugins[i];

						if (configuration.TargetPlatform.Module.HasDefaultTarget)
						{
							configuration.TargetEmitter = configuration.TargetPlatform.Module.DefaultTarget;
						}
						else if (configuration.TargetPlatform.Emitters.Count == 1)
						{
							configuration.TargetEmitter = configuration.TargetPlatform.Emitters[0];
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
