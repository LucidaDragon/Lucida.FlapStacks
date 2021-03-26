using System;
using System.Collections.Generic;
using System.Text;

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bzr : Instruction
	{
		public override string Keyword => "bzr";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bzr();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[1].Push(e);
			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchZero();

			e.MarkLabel(onFalse);
		}
	}
}
