namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class MarkLabelInst : Instruction
	{
		public override string Keyword => $"{Label.Name}:";

		private Label Label;

		protected override int ArgumentCount => 0;

		public MarkLabelInst() { }

		public MarkLabelInst(Label label)
		{
			Label = label;
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
			var label = new MarkLabelInst
			{
				Label = new Label() { Name = keyword.Substring(1) }
			};

			return label;
		}
	}
}
