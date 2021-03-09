using System.Collections.Generic;

namespace Lucida.FlapStacks.Plugins
{
	public class Plugin
	{
		public ModuleDefinition Module { get; }
		public IReadOnlyList<EmitSource> Parsers { get; }
		public IReadOnlyList<Emitter> Emitters { get; }
		public IReadOnlyList<Device> Devices { get; }

		public Plugin(ModuleDefinition module, IReadOnlyList<EmitSource> parsers, IReadOnlyList<Emitter> emitters, IReadOnlyList<Device> devices)
		{
			Module = module;
			Parsers = parsers;
			Emitters = emitters;
			Devices = devices;
		}
	}
}
