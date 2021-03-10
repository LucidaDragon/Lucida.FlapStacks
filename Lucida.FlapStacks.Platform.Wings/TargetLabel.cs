namespace Lucida.FlapStacks.Platform.Wings
{
	public class TargetLabel : Value
	{
		public string Name { get; }

		private readonly Instruction[] Instructions;

		public TargetLabel(string name, Instruction[] instructions)
		{
			Name = name;
			Instructions = instructions;
		}

		public override ulong Get()
		{
			for (int i = 0; i < Instructions.Length; i++)
			{
				var inst = Instructions[i];

				if (inst.IsTarget(Name))
				{
					return inst.GetTargetValue();
				}
			}

			return ulong.MaxValue;
		}
	}
}
