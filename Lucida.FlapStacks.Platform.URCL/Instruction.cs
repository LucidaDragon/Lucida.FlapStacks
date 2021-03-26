namespace Lucida.FlapStacks.Platform.URCL
{
	public abstract class Instruction
	{
		public abstract string Keyword { get; }

		protected abstract int OperandCount { get; }

		public Operand[] Operands { get; }

		public ulong MaxRegister
		{
			get
			{
				var result = 0UL;

				for (int i = 0; i < Operands.Length; i++)
				{
					var max = Operands[i].MaxRegister;

					if (max > result) result = max;
				}

				return result;
			}
		}

		public Instruction()
		{
			Operands = new Operand[OperandCount];
		}

		protected abstract Instruction CreateNew(string keyword);

		protected abstract void EmitCore(UrclConfig config, Emitter e);

		public virtual Instruction Create(Parser parser, UrclConfig config, string keyword, Operand[] operands)
		{
			var result = CreateNew(keyword);

			for (int i = 0; i < OperandCount && i < operands.Length; i++)
			{
				result.Operands[i] = operands[i];
			}

			return result;
		}

		public virtual void Emit(UrclConfig config, Emitter e)
		{
			if (Operands.Length >= 2)
			{
				for (int i = 1; i < Operands.Length; i++)
				{
					Operands[i].Push(e);
				}
			}

			if (Operands.Length == 1)
			{
				Operands[0].Push(e);
			}

			EmitCore(config, e);

			if (Operands.Length >= 2)
			{
				Operands[0].Pop(e);
			}
		}

		public virtual bool IsValid(string keyword, int operands)
		{
			return IsKeyword(keyword) && OperandCount == operands;
		}

		protected virtual bool IsKeyword(string keyword)
		{
			return Keyword.ToLower() == keyword.ToLower();
		}

		public virtual string GetString()
		{
			return $"{Keyword} {string.Join(", ", GetValueStrings(Operands))}";
		}

		/// <summary>
		/// Stack: -> Value{0..1}
		/// </summary>
		protected void LoadCarry(Emitter e)
		{
			e.Push(new Constant(0));
			e.LoadHeap();
		}

		/// <summary>
		/// Stack: ->
		/// </summary>
		protected void ClearCarry(Emitter e)
		{
			e.Push(new Constant(0));
			StoreCarry(e);
		}

		/// <summary>
		/// Stack: ->
		/// </summary>
		protected void SetCarry(Emitter e)
		{
			e.Push(new Constant(1));
			StoreCarry(e);
		}

		/// <summary>
		/// Stack: Value{0..1} ->
		/// </summary>
		protected void StoreCarry(Emitter e)
		{
			e.Push(new Constant(0));
			e.StoreHeap();
		}

		/// <summary>
		/// Stack: -> Value{Any}
		/// </summary>
		protected void LoadResult(Emitter e)
		{
			e.Push(new Constant(1));
			e.LoadHeap();
		}

		/// <summary>
		/// Stack: Value{Any} -> Value{Any}
		/// </summary>
		protected void SaveResult(Emitter e)
		{
			e.Duplicate();
			StoreResult(e);
		}

		/// <summary>
		/// Stack: Value{Any} ->
		/// </summary>
		protected void StoreResult(Emitter e)
		{
			e.Push(new Constant(1));
			e.StoreHeap();
		}

		private static string[] GetValueStrings(Operand[] values)
		{
			var result = new string[values.Length];

			for (int i = 0; i < values.Length; i++)
			{
				result[i] = values[i].GetString();
			}

			return result;
		}
	}
}
