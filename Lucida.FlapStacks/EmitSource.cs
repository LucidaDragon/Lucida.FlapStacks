namespace Lucida.FlapStacks
{
	public abstract class EmitSource
	{
		public abstract string Name { get; }

		public abstract void Load(Stream stream);
		public abstract void EmitTo(Emitter emitter);
	}
}
