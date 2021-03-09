namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfeInst : Instruction
	{
		public override string Keyword => "ife";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.BranchEqual();
		}

		protected override Instruction CreateNew()
		{
			return new IfeInst();
		}
	}
}
