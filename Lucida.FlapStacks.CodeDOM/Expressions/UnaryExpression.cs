using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class UnaryExpression : Expression
	{
		public override Type ResultType => Operator.ResultType;

		public UnaryOperator Operator { get; set; }

		public Expression Source { get; set; }

		public override bool IsConstant => Source.IsConstant;
		public override Value Constant => Operator.Compute(Source.Constant);

		public override void Emit(Emitter emitter)
		{
			if (Source.ResultType != Operator.SourceType) throw new Exception("Operand does not match operator.");

			Source.Emit(emitter);
			Operator.Emit(emitter);
		}
	}
}
