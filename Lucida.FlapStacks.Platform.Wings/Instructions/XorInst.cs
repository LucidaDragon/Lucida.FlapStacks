namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class XorInst : Instruction
	{
		public override string Keyword => "xor";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Xor();
		}

		protected override Instruction CreateNew()
		{
			return new XorInst();
		}
	}
}
