namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class DataWordOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Value Value { get; }

		public DataWordOp(Value value)
		{
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteLittleEndian((ushort)Value.Get());
		}
	}
}
