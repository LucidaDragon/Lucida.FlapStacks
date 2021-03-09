namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class PopOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 1;

		public Register Target { get; }

		public PopOp(Register target)
		{
			Target = target;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x58 + (int)Target));
		}
	}
}
