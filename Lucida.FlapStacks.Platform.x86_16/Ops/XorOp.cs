namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class XorOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public Register Source { get; }

		public XorOp(Register target, Register source)
		{
			Target = target;
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x33);
			stream.WriteByte((byte)(0xC0 + ((int)Target << 3) + (int)Source));
		}
	}
}
