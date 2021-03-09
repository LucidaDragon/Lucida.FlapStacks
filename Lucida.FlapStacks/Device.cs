namespace Lucida.FlapStacks
{
	public abstract class Device
	{
		public ulong Tag { get; set; } = 0;

		public abstract string Name { get; }

		public abstract void BeginUse(Emitter e);

		/// <summary>
		///	Stack: OnSuccess{Address}, OnError{Address} -> Value{Any}
		/// </summary>
		public abstract void Read(Emitter e);

		/// <summary>
		/// Stack: Value{Any}, OnSuccess{Address}, OnError{Address} ->
		/// </summary>
		public abstract void Write(Emitter e);

		public abstract void EndUse(Emitter e);

		public abstract Device CreateNew();
	}

	public abstract class Device<T> : Device where T : Emitter
	{
		public override void BeginUse(Emitter e)
		{
			if (e is T emit) BeginUse(emit);
		}

		public override void Read(Emitter e)
		{
			if (e is T emit) Read(emit);
		}

		public override void Write(Emitter e)
		{
			if (e is T emit) Write(emit);
		}

		public override void EndUse(Emitter e)
		{
			if (e is T emit) EndUse(emit);
		}

		public abstract void BeginUse(T e);

		/// <summary>
		///	Stack: OnSuccess{Address}, OnError{Address} -> Value{Any}
		/// </summary>
		public abstract void Read(T e);

		/// <summary>
		/// Stack: Value{Any}, OnSuccess{Address}, OnError{Address} ->
		/// </summary>
		public abstract void Write(T e);

		public abstract void EndUse(T e);
	}
}
