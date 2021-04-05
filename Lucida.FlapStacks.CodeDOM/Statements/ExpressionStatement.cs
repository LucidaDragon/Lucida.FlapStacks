namespace Lucida.FlapStacks.CodeDOM.Statements
{
	public abstract class ExpressionStatement : Statement
	{
		public Expression Expression { get; set; }

		public override void Emit(Emitter emitter)
		{
			Expression.Emit(emitter);

			for (ulong i = 0; i < Expression.ResultType.Size; i++)
			{
				emitter.Pop();
			}
		}
	}
}
