namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class AddcInst : Instruction
	{
		public override string Keyword => "addc";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.AddCarry();
		}

		protected override Instruction CreateNew()
		{
			return new AddcInst();
		}
	}
}
