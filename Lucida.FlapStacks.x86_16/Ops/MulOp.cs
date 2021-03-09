namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class MulOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Source { get; }

		public MulOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xF7);
			stream.WriteByte((byte)(0xE0 + (int)Source));
		}
	}
}
