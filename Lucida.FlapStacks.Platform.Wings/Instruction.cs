using Lucida.FlapStacks.Platform.Wings.Instructions;

namespace Lucida.FlapStacks.Platform.Wings
{
	public abstract class Instruction
	{
		public static readonly Instruction[] Instructions = new Instruction[]
		{
			new AdcInst(),
			new AddcInst(),
			new AddInst(),
			new AndInst(),
			new BoolInst(),
			new BppopInst(),
			new BppushInst(),
			new BpsetInst(),
			new CallInst(),
			new DivInst(),
			new DupInst(),
			new GotoInst(),
			new HeapInst(),
			new IfeInst(),
			new IfsgInst(),
			new IfslInst(),
			new IfugInst(),
			new IfulInst(),
			new IfzInst(),
			new LodInst(),
			new LodsInst(),
			new LshInst(),
			new MarkLabelInst(null),
			new MulInst(),
			new NegInst(),
			new NopInst(),
			new NotInst(),
			new OrInst(),
			new PopInst(),
			new PushInst(),
			new RawByteInst(),
			new RemInst(),
			new RshInst(),
			new SbbInst(),
			new StrInst(),
			new StrsInst(),
			new SubbInst(),
			new SubInst(),
			new SwapInst(),
			new XorInst()
		};

		public abstract string Keyword { get; }
		public Value[] Arguments { get; }

		protected abstract int ArgumentCount { get; }

		public Instruction()
		{
			Arguments = new Value[ArgumentCount];
		}

		public virtual void PreEmit(Emitter emitter) { }

		public virtual bool IsTarget(string targetName)
		{
			return false;
		}

		public virtual ulong GetTargetValue()
		{
			return 0;
		}

		public virtual bool IsValid(string keyword, int args)
		{
			return Keyword == keyword.ToLower() && args == Arguments.Length;
		}

		public virtual Instruction Create(string keyword, Value[] args)
		{
			var result = CreateNew();

			for (int i = 0; i < result.Arguments.Length; i++)
			{
				result.Arguments[i] = args[i];
			}

			return result;
		}

		protected abstract Instruction CreateNew();

		public abstract void Emit(Emitter emitter);

		public string GetString()
		{
			return $"{Keyword} {string.Join(", ", GetArgumentStrings())}".Trim();
		}

		private string[] GetArgumentStrings()
		{
			var result = new string[Arguments.Length];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = $"0x{Arguments[i].Get():X}";
			}

			return result;
		}
	}
}
