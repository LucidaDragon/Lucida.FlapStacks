namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class Imm16Op : Op
	{
		public override int GetSize(Emitter8086 emitter) => 3;

		public Register Target { get; }
		public Value Value { get; }

		public Imm16Op(Register target, ulong value) : this(target, new Constant(value)) { }

		public Imm16Op(Register target, long value) : this(target, new Constant(value)) { }

		public Imm16Op(Register target, Value value)
		{
			Target = target;
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			var value = Value.Get();
			stream.WriteByte((byte)(0xB8 + (int)Target));
			stream.WriteByte((byte)value);
			stream.WriteByte((byte)(value >> 8));
		}
	}
}
