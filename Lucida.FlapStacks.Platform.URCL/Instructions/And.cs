﻿namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class And : Instruction
	{
		public override string Keyword => "and";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new And();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.And();
			SaveResult(e);
		}
	}
}
