namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class RshOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }

		public RshOp(Register target)
		{
			Target = target;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xD3);
			stream.WriteByte((byte)(0xE8 + (int)Target));
		}
	}
}
