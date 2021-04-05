using Lucida.FlapStacks.Plugins;
using System;

namespace Lucida.FlapStacks.Compiler
{
	public class Configuration
	{
		public List<Plugin> Plugins { get; private set; } = new List<Plugin>();
		public List<Action> OnLoad { get; private set; } = new List<Action>();
		public List<Action> OnPreCompile { get; private set; } = new List<Action>();
		public List<Action> OnPostCompile { get; private set; } = new List<Action>();

		public Stream Source { get; set; }
		public Plugin SourcePlatform { get; set; }
		public EmitSource SourceParser { get; set; }

		public List<Device> Devices { get; set; } = new List<Device>();

		public Plugin TargetPlatform { get; set; }
		public Emitter TargetEmitter { get; set; }
		public Stream Target { get; set; }

		public int Execute()
		{
			try
			{
				for (int i = 0; i < OnLoad.Count; i++)
				{
					OnLoad[i]();
				}

				for (int i = 0; i < OnPreCompile.Count; i++)
				{
					OnPreCompile[i]();
				}

				if (Source == null) throw new Exception("No source is defined.");
				if (SourcePlatform == null) throw new Exception("Source platform is not defined.");
				if (SourceParser == null) throw new Exception("Compile source is not defined.");
				if (TargetPlatform == null) throw new Exception("Target platform is not defined.");
				if (TargetEmitter == null) throw new Exception("Target emitter is ambiguous or undefined.");
				if (Target == null) throw new Exception("No output is defined.");

				Console.WriteLine($"Beginning compile from \"{SourcePlatform.Module.ID}/{SourceParser.Name}\" to \"{TargetPlatform.Module.ID}/{TargetEmitter.Name}\"...");
				var start = Environment.TickCount64;

				SourceParser.Load(Source);

				TargetEmitter.Devices = Devices;

				for (int i = 0; i < Devices.Count; i++)
				{
					Devices[i].BeginUse(TargetEmitter);
				}

				SourceParser.EmitTo(TargetEmitter);

				for (int i = Devices.Count - 1; i >= 0; i--)
				{
					Devices[i].EndUse(TargetEmitter);
				}

				TargetEmitter.Save(Target);

				if (OnPostCompile.Count > 0) Console.WriteLine($"Successfully compiled in {Environment.TickCount64 - start}ms.");

				for (int i = 0; i < OnPostCompile.Count; i++)
				{
					OnPostCompile[i]();
				}

				Console.WriteLine($"Successfully completed in {Environment.TickCount64 - start}ms.");

				return 0;
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				return 1;
			}
		}
	}
}
