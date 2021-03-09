namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class DataByteOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => Count;

		public byte Value { get; }
		public int Count { get; }

		public DataByteOp(byte value, int count = 1)
		{
			Value = value;
			Count = count;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			for (int i = 0; i < Count; i++)
			{
				stream.WriteByte(Value);
			}
		}
	}
}
