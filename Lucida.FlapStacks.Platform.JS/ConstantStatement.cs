namespace Lucida.FlapStacks.Platform.JS
{
	public class ConstantStatement : Statement
	{
		private readonly string Statement;

		public ConstantStatement(string statement)
		{
			Statement = statement;
		}

		public override string GetString()
		{
			return Statement;
		}
	}
}
