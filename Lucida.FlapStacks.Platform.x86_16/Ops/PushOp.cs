namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class PushOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 1;

		public Register Source { get; }

		public PushOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x50 + (int)Source));
		}
	}
}
