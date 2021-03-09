namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class SubInst : Instruction
	{
		public override string Keyword => "sub";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Subtract();
		}

		protected override Instruction CreateNew()
		{
			return new SubInst();
		}
	}
}
