namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfslInst : Instruction
	{
		public override string Keyword => "ifsl";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.BranchSignedLess();
		}

		protected override Instruction CreateNew()
		{
			return new IfslInst();
		}
	}
}
