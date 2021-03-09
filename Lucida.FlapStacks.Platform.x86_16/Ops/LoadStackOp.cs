namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class LoadStackOp : LoadOp
	{
		public override int GetSize(Emitter8086 emitter) => 3;

		public LoadStackOp(Register target) : base(target) { }

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte(0x36);
			base.Emit(emitter, stream);
		}
	}
}
