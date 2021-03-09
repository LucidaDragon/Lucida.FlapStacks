namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class PushImmOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 3;

		public Value Value { get; }

		public PushImmOp(Value value)
		{
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x68);
			stream.WriteLittleEndian((ushort)Value.Get());
		}
	}
}
