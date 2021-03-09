namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class StoreStackOp : StoreOp
	{
		public override int GetSize(Emitter8086 emitter) => 3;

		public StoreStackOp(Register source) : base(source) { }

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x36);
			base.Emit(emitter, stream);
		}
	}
}
