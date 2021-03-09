namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class SubOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public Register Source { get; }
		public bool WithBorrow { get; }

		public SubOp(Register target, Register source, bool withBorrow = false)
		{
			Target = target;
			Source = source;
			WithBorrow = withBorrow;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x03 + (WithBorrow ? 0x18 : 0x28)));
			stream.WriteByte((byte)(0xC0 + ((int)Target << 3) + (int)Source));
		}
	}
}
