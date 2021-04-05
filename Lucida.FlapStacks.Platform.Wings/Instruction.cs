using System;

namespace Lucida.FlapStacks.Platform.Wings
{
	public abstract class Instruction
	{
		public static readonly Instruction[] Instructions;

		static Instruction()
		{
			var insts = new List<Instruction>();

			foreach (var type in typeof(Instruction).Assembly.GetTypes())
			{
				if (!type.IsAbstract && typeof(Instruction).IsAssignableFrom(type))
				{
					insts.Add((Instruction)Activator.CreateInstance(type));
				}
			}

			Instructions = insts.ToArray();
		}

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
