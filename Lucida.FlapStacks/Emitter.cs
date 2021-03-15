namespace Lucida.FlapStacks
{
	public abstract class Emitter
	{
		public abstract string Name { get; }

		public List<Device> Devices { get; set; } = new List<Device>();

		public abstract void Save(Stream stream);

		public abstract Value CreateLabel();

		public abstract void MarkLabel(Value label);

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Add();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Subtract();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Multiply();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Divide();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Remainder();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void LeftShift();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void RightShift();

		/// <summary>
		/// Stack: Value{Any} -> Result{Any}
		/// </summary>
		public abstract void Negate();

		/// <summary>
		/// Stack: Value{Any} -> Result{Any}
		/// </summary>
		public abstract void Not();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void And();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Or();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Result{Any}
		/// </summary>
		public abstract void Xor();

		/// <summary>
		/// Stack: Value{Any} -> Value{0..1}
		/// </summary>
		public abstract void Bool();

		/// <summary>
		/// Stack: -> Value{Any}
		/// </summary>
		public abstract void Push(Value value);

		/// <summary>
		/// Stack: -> Value{-128..127}
		/// </summary>
		public void Push(sbyte value)
		{
			Push(new Constant(value));
		}

		/// <summary>
		/// Stack: -> Value{0}
		/// </summary>
		public void PushZero()
		{
			Push(new Constant(0));
		}

		/// <summary>
		/// Stack: Value{Any} ->
		/// </summary>
		public abstract void Pop();

		/// <summary>
		/// Stack: Value{Any} -> Value{Any}, Value{Any}
		/// </summary>
		public abstract void Duplicate();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any} -> Value2{Any}, Value1{Any}
		/// </summary>
		public abstract void Swap();

		/// <summary>
		/// Stack: Next{Address} ->
		/// </summary>
		public abstract void Goto();

		/// <summary>
		/// Stack: Value{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchZero();

		/// <summary>
		/// Stack: Value{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchNotZero()
		{
			Swap();
			BranchZero();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchEqual();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchNotEqual()
		{
			Swap();
			BranchEqual();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchUnsignedLess();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchUnsignedGreater();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchUnsignedLessOrEqual()
		{
			Swap();
			BranchUnsignedGreater();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchUnsignedGreaterOrEqual()
		{
			Swap();
			BranchUnsignedLess();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchSignedLess();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public abstract void BranchSignedGreater();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchSignedLessOrEqual()
		{
			Swap();
			BranchSignedGreater();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} ->
		/// </summary>
		public void BranchSignedGreaterOrEqual()
		{
			Swap();
			BranchSignedLess();
		}

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} -> Result{Any}
		/// </summary>
		public abstract void AddCarry();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} -> Result{Any}
		/// </summary>
		public abstract void AddWithCarry();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} -> Result{Any}
		/// </summary>
		public abstract void SubtractBorrow();

		/// <summary>
		/// Stack: Value1{Any}, Value2{Any}, OnTrue{Address}, OnFalse{Address} -> Result{Any}
		/// </summary>
		public abstract void SubtractWithBorrow();

		/// <summary>
		/// Stack: -> BasePointer{Stack}
		/// </summary>
		public abstract void PushBasePointer();

		/// <summary>
		/// Stack: BasePointer{Stack} ->
		/// </summary>
		public abstract void PopBasePointer();

		/// <summary>
		/// Stack: ->
		/// </summary>
		public abstract void SetBasePointer();

		/// <summary>
		/// Stack: Offset{Any} -> Value{Any}
		/// </summary>
		public abstract void LoadStack();

		/// <summary>
		/// Stack: Value{Any}, Offset{Any} ->
		/// </summary>
		public abstract void StoreStack();

		/// <summary>
		/// Stack: Call{Address} -> Return{Address}
		/// </summary>
		public abstract void Call();

		/// <summary>
		/// Stack: Call{Address} -> Value{Any}
		/// </summary>
		public void CallFunction()
		{
			PushZero();
			Swap();
			PushBasePointer();
			Swap();
			SetBasePointer();
			Call();
			PopBasePointer();
		}

		/// <summary>
		/// Stack: Argument{0..Args-1} -> Value{Any}
		/// </summary>
		public void LoadFunctionArg()
		{
			ArgumentOffset();
			LoadStack();
		}

		/// <summary>
		/// Stack: Value{Any}, Argument{0..Args-1} ->
		/// </summary>
		public void StoreFunctionArg()
		{
			ArgumentOffset();
			StoreStack();
		}

		/// <summary>
		/// Stack: Value{Any} ->
		/// </summary>
		public void StoreFunctionResult()
		{
			Push(2);
			StoreStack();
		}

		/// <summary>
		/// Stack: Offset{Any} -> Value{Any}
		/// </summary>
		public abstract void LoadHeap();

		/// <summary>
		/// Stack: Value{Any}, Offset{Any} ->
		/// </summary>
		public abstract void StoreHeap();

		/// <summary>
		/// Stack: -> MaxOffset{Any}
		/// </summary>
		public abstract void MaxHeap();

		/// <summary>
		///	Stack: OnSuccess{Address}, OnError{Address} -> Value{Any}
		/// </summary>
		public void ReadDevice(ulong tag)
		{
			for (int i = 0; i < Devices.Count; i++)
			{
				var dev = Devices[i];

				if (dev.Tag == tag)
				{
					dev.Read(this);
					return;
				}
			}
		}

		/// <summary>
		/// Stack: Value{Any}, OnSuccess{Address}, OnError{Address} ->
		/// </summary>
		public void WriteDevice(ulong tag)
		{
			for (int i = 0; i < Devices.Count; i++)
			{
				var dev = Devices[i];

				if (dev.Tag == tag)
				{
					dev.Write(this);
					return;
				}
			}
		}

		public abstract void WriteByte(byte value);

		public void WriteByte(sbyte value)
		{
			WriteByte((byte)value);
		}

		public abstract void WriteWord(Value value);

		public void WriteWord(ulong value)
		{
			WriteWord(new Constant(value));
		}

		public void WriteWord(long value)
		{
			WriteWord((ulong)value);
		}

		/// <summary>
		/// Stack: Argument{0...Args-1} -> Offset{Any}
		/// </summary>
		private void ArgumentOffset()
		{
			Push(3);
			Add();
		}
	}
}
