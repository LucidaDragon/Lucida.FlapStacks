namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class InterruptOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public byte Value { get; }

		public InterruptOp(byte value)
		{
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xCD);
			stream.WriteByte(Value);
		}
	}
}
