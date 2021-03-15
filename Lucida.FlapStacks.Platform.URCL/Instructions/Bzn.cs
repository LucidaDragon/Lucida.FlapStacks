namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bzn : Instruction
	{
		public override string Keyword => "bzn";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bzn();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			Operands[1].Push(e);
			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchNotZero();

			e.MarkLabel(onFalse);
		}
	}
}
