namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class LoadAddrAxOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 3;

		public Value Value { get; }

		public LoadAddrAxOp(Value value)
		{
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xA1);
			stream.WriteLittleEndian((ushort)Value.Get());
		}
	}
}
