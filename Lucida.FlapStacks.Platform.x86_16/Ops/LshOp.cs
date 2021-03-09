namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class LshOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }

		public LshOp(Register target)
		{
			Target = target;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xD3);
			stream.WriteByte((byte)(0xE0 + (int)Target));
		}
	}
}
