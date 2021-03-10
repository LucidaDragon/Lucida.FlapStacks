namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class MarkLabelInst : Instruction
	{
		public override string Keyword => $"{Name}:";

		private Value Label;
		private readonly string Name;

		protected override int ArgumentCount => 0;

		public MarkLabelInst(string name)
		{
			Name = name;
		}

		public override bool IsTarget(string targetName)
		{
			return Name == targetName;
		}

		public override ulong GetTargetValue()
		{
			return Label.Get();
		}

		public override void PreEmit(Emitter emitter)
		{
			Label = emitter.CreateLabel();
		}

		public override void Emit(Emitter emitter)
		{
			emitter.MarkLabel(Label);
		}

		protected override Instruction CreateNew()
		{
			return null;
		}

		public override bool IsValid(string keyword, int args)
		{
			return keyword.EndsWith(":") && keyword.Length > 1 && args == 0;
		}

		public override Instruction Create(string keyword, Value[] args)
		{
			return new MarkLabelInst(keyword.Substring(0, keyword.Length - 1));
		}
	}
}
