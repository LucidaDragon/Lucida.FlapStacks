namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class SubbInst : Instruction
	{
		public override string Keyword => "subb";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.SubtractBorrow();
		}

		protected override Instruction CreateNew()
		{
			return new SubbInst();
		}
	}
}
