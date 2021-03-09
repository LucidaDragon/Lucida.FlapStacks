using System;
using System.Collections.Generic;
using System.Text;

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class IfsgInst : Instruction
	{
		public override string Keyword => "ifsg";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			throw new NotImplementedException();
		}

		protected override Instruction CreateNew()
		{
			throw new NotImplementedException();
		}
	}
}
