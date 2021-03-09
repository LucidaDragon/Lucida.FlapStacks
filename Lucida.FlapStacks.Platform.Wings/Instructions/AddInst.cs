namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class AddInst : Instruction
	{
		public override string Keyword => "add";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Add();
		}

		protected override Instruction CreateNew()
		{
			return new AddInst();
		}
	}
}
