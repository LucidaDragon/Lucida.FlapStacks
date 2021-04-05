using System;

namespace Lucida.FlapStacks.Platform.JS
{
	public class DynamicStatement : Statement
	{
		private readonly Func<Value[], string> Create;
		private readonly Value[] Values;

		public DynamicStatement(Func<Value[], string> create, params Value[] values)
		{
			Create = create;
			Values = values;
		}

		public override string GetString()
		{
			return Create(Values);
		}
	}
}
