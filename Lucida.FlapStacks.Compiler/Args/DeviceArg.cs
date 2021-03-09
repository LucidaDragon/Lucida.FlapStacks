using Lucida.FlapStacks.Plugins;
using System;

namespace Lucida.FlapStacks.Compiler.Args
{
	public class DeviceArg : ArgHandler
	{
		public override string[] ArgNames => new[] { "device", "dev", "d" };

		public override string ParameterFormat => "<device port> <device name>";

		public override string HelpText => "Attach a device driver to the specified port.";

		public override bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length != 2 || !ulong.TryParse(args[0], out ulong tag)) return false;

			var name = args[1];
			configuration.OnPreCompile.Add(() =>
			{
				var platform = configuration.TargetPlatform;

				if (platform != null && TryAddDevice(configuration, platform, name, tag)) return;

				platform = configuration.SourcePlatform;

				if (platform != null && TryAddDevice(configuration, platform, name, tag)) return;

				throw new Exception($"No device named \"{name}\" is defined.");
			});

			return true;
		}

		private static bool TryAddDevice(Configuration configuration, Plugin platform, string name, ulong tag)
		{
			for (int i = 0; i < platform.Devices.Count; i++)
			{
				var dev = platform.Devices[i];

				if (dev.Name == name)
				{
					dev = dev.CreateNew();
					dev.Tag = tag;
					configuration.Devices.Add(dev);
					return true;
				}
			}

			return false;
		}
	}
}
