namespace Lucida.FlapStacks.Platform.URCL.Instructions.Multicore
{
	public class Join : Instruction
	{
		public override string Keyword => "join";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Join();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Push(e);
			e.Join();
		}
	}
}
