namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfugInst : Instruction
	{
		public override string Keyword => "ifug";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.BranchUnsignedGreater();
		}

		protected override Instruction CreateNew()
		{
			return new IfugInst();
		}
	}
}
