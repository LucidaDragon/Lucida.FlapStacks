namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class LabelOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 0;

		public override void Emit(Emitter8086 emitter, Stream stream) { }
	}
}
