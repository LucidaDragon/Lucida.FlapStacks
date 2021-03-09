namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class RetOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 1;

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0xC3);
		}
	}
}
