namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class CallOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Source { get; }

		public CallOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xFF);
			stream.WriteByte((byte)(0xD0 + (int)Source));
		}
	}
}
