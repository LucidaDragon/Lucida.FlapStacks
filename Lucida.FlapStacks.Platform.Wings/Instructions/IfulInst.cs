namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfulInst : Instruction
	{
		public override string Keyword => "iful";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.BranchUnsignedLess();
		}

		protected override Instruction CreateNew()
		{
			return new IfulInst();
		}
	}
}
