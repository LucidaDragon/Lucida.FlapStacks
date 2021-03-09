namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class Imm8Op : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public byte Value { get; }

		public Imm8Op(Register target, byte value)
		{
			Target = target;
			Value = value;
		}

		public Imm8Op(Register target, sbyte value) : this(target, (byte)value) { }

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x50 + (int)Target));
			stream.WriteByte(Value);
		}
	}
}
