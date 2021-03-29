using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class BinaryExpression : Expression
	{
		public override Type ResultType => Operator.ResultType;

		public BinaryOperator Operator { get; set; }

		public Expression A { get; set; }
		public Expression B { get; set; }

		public override bool IsConstant => A.IsConstant && B.IsConstant;
		public override Value Constant => Operator.Compute(A.Constant, B.Constant);

		public override void Emit(Emitter emitter)
		{
			if (A.ResultType != Operator.AType) throw new Exception("First operand does not match operator.");
			if (B.ResultType != Operator.BType) throw new Exception("Second operand does not match operator.");

			A.Emit(emitter);
			B.Emit(emitter);
			Operator.Emit(emitter);
		}
	}
}
