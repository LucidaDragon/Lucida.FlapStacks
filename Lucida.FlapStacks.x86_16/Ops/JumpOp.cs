namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class JumpOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Source { get; }

		public JumpOp(Register source)
		{
			Source = source;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xFF);
			stream.WriteByte((byte)(0xE0 + (int)Source));
		}
	}
}
