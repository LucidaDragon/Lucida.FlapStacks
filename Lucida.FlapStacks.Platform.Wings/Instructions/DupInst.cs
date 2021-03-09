namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class DupInst : Instruction
	{
		public override string Keyword => "dup";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Duplicate();
		}

		protected override Instruction CreateNew()
		{
			return new DupInst();
		}
	}
}
