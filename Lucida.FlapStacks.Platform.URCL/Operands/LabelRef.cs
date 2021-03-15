using System;

namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class LabelRef : Operand
	{
		public Label Label { get; }

		public override Value Value => Label;

		private readonly Parser Parent;

		public LabelRef(Parser parent)
		{
			Parent = parent;
		}

		public LabelRef(Label label)
		{
			Label = label;
		}

		public override string GetString()
		{
			return $".{Label.Name}";
		}

		public override void Pop(Emitter e)
		{
			throw new Exception("A label can not be the target of an operation.");
		}

		public override void Push(Emitter e)
		{
			e.Push(Label);
		}

		public override bool TryParse(string str, out Operand operand)
		{
			if (Label.TryParse(str, out string name) && Parent != null && Parent.Labels.TryGetValue(name, out Label label))
			{
				operand = new LabelRef(label);
				return true;
			}
			else
			{
				operand = null;
				return false;
			}
		}
	}
}
