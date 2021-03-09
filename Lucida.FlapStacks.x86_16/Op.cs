namespace Lucida.FlapStacks.Platform.x86_16
{
	public abstract class Op
	{
		public abstract int GetSize(Emitter8086 emitter);
		
		public abstract void Emit(Emitter8086 emitter, Stream stream);
	}
}
