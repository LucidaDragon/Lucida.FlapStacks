namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class JumpConditionalOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Condition Condition { get; }
		public sbyte Offset { get; }

		public JumpConditionalOp(Condition condition, sbyte offset)
		{
			Condition = condition;
			Offset = offset;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x70 + (int)Condition));
			stream.WriteByte(Offset);
		}
	}
}
