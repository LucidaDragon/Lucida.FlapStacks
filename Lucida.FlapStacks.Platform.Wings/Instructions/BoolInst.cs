namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class BoolInst : Instruction
	{
		public override string Keyword => "bool";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Bool();
		}

		protected override Instruction CreateNew()
		{
			return new BoolInst();
		}
	}
}
