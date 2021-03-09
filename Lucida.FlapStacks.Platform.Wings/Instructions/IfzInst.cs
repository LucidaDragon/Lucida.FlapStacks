namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfzInst : Instruction
	{
		public override string Keyword => "ifz";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.BranchZero();
		}

		protected override Instruction CreateNew()
		{
			return new IfzInst();
		}
	}
}
