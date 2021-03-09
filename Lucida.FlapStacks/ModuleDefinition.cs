namespace Lucida.FlapStacks
{
	public abstract class ModuleDefinition
	{
		public string ID => $"{Author}.{Platform}:{Version}";
		public string Platform { get; }
		public string Author { get; }
		public string Version => $"{MajorVersion}.{MinorVersion}";
		public uint MajorVersion { get; }
		public uint MinorVersion { get; }

		public bool HasDefaultSource => DefaultSource != null;
		public EmitSource DefaultSource { get; }
		public bool HasDefaultTarget => DefaultTarget != null;
		public Emitter DefaultTarget { get; }

		public ModuleDefinition(string platform, string author, uint majorVersion, uint minorVersion, EmitSource defaultSource = null, Emitter defaultTarget = null)
		{
			Platform = platform;
			Author = author;
			MajorVersion = majorVersion;
			MinorVersion = minorVersion;

			DefaultSource = defaultSource;
			DefaultTarget = defaultTarget;
		}
	}
}
