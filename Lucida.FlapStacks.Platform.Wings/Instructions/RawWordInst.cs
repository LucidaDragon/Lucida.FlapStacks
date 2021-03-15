namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class RawWordInst : Instruction
	{
		public override string Keyword => "dw";

		protected override int ArgumentCount => 1;

		public RawWordInst() { }

		public RawWordInst(Value value)
		{
			Arguments[0] = value;
		}

		public override void Emit(Emitter emitter)
		{
			emitter.WriteWord(Arguments[0]);
		}

		protected override Instruction CreateNew()
		{
			return new RawWordInst();
		}
	}
}
