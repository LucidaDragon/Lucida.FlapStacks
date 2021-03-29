using System;

namespace Lucida.FlapStacks.CodeDOM.Statements
{
	public abstract class WhileStatement : Statement
	{
		public Expression Condition { get; set; }
		public List<Statement> Body { get; set; }

		public override void Emit(Emitter emitter)
		{
			if (Condition.ResultType.Size != 1) throw new Exception("Condition result type must have a size of 1.");

			var loop = emitter.CreateLabel();
			var body = emitter.CreateLabel();
			var end = emitter.CreateLabel();

			emitter.MarkLabel(loop);
			Condition.Emit(emitter);
			emitter.Push(end);
			emitter.Push(body);
			emitter.BranchZero();
			emitter.MarkLabel(body);

			for (int i = 0; i < Body.Count; i++)
			{
				Body[i].Emit(emitter);
			}

			emitter.Push(loop);
			emitter.Goto();
			emitter.MarkLabel(end);
		}
	}
}
