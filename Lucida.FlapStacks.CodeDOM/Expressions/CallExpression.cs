using Lucida.FlapStacks.CodeDOM.Types;
using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class CallExpression : Expression
	{
		public Expression Target { get; set; }
		public Expression[] Arguments { get; set; }

		public override Type ResultType => (Target.ResultType as FunctionType).ReturnType;

		public override void Emit(Emitter emitter)
		{
			if (Target.ResultType.Size != 1) throw new Exception("Target result type must have a size of 1.");
			if (!(Target.ResultType.PointerType is FunctionType func)) throw new Exception("Target pointer type must be a function.");
			if (Arguments.Length == func.Fields.Length) throw new Exception("Number of arguments must mach number of function fields.");
			if (func.ReturnType.Size > 1) throw new Exception("Function return type must have a size of 0 or 1.");

			for (int i = 0; i < Arguments.Length; i++)
			{
				var arg = Arguments[i];

				if (arg.ResultType.Size != 1) throw new Exception($"Argument result type at index {i} must have a size of 1.");

				arg.Emit(emitter);
			}

			Target.Emit(emitter);
			emitter.CallFunction();

			if (func.ReturnType.Size == 0) emitter.Pop();
		}
	}
}
