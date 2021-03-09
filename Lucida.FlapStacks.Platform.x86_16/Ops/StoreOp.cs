namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class StoreOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Source { get; }

		public StoreOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x89);
			stream.WriteByte((byte)(0x7 + ((int)Source << 3)));
		}
	}
}
