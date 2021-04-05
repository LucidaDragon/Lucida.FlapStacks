using System;

namespace Lucida.FlapStacks.CodeDOM.Statements
{
	public abstract class IfStatement : Statement
	{
		public Expression Condition { get; set; }
		public List<Statement> TrueBody { get; set; } = new List<Statement>();
		public List<Statement> FalseBody { get; set; } = new List<Statement>();

		public override void Emit(Emitter emitter)
		{
			if (Condition.ResultType.Size != 1) throw new Exception("Condition result type must have a size of 1.");

			if (TrueBody.Count > 0 || FalseBody.Count > 0)
			{
				var onTrue = emitter.CreateLabel();
				var onFalse = emitter.CreateLabel();
				var end = emitter.CreateLabel();

				Condition.Emit(emitter);
				emitter.Push(onFalse);
				emitter.Push(TrueBody.Count > 0 ? onTrue : end);
				emitter.BranchZero();

				emitter.MarkLabel(onTrue);

				for (int i = 0; i < TrueBody.Count; i++)
				{
					TrueBody[i].Emit(emitter);
				}

				if (FalseBody.Count > 0)
				{
					emitter.Push(end);
					emitter.Goto();
				}

				emitter.MarkLabel(onFalse);

				for (int i = 0; i < FalseBody.Count; i++)
				{
					FalseBody[i].Emit(emitter);
				}

				emitter.MarkLabel(end);
			}
		}
	}
}
