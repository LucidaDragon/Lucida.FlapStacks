namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class NegOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Source { get; }

		public NegOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xF7);
			stream.WriteByte((byte)(0xD8 + (int)Source));
		}
	}
}
