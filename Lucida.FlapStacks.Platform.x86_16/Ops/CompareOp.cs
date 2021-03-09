namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class CompareOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public Register Source { get; }

		public CompareOp(Register target, Register source)
		{
			Target = target;
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x3B);
			stream.WriteByte((byte)(0xC0 + ((int)Target << 3) + (int)Source));
		}
	}
}
