namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class LowOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }

		public LowOp(Register target)
		{
			Target = target;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x32);
			stream.WriteByte((byte)(0xE4 + ((int)Target << 3) + (int)Target));
		}
	}
}
