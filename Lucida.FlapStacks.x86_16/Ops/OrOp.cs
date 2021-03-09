namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class OrOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public Register Source { get; }

		public OrOp(Register target, Register source)
		{
			Target = target;
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x0B);
			stream.WriteByte((byte)(0xC0 + ((int)Target << 3) + (int)Source));
		}
	}
}
