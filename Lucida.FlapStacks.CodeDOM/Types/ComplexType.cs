namespace Lucida.FlapStacks.CodeDOM.Types
{
	public class ComplexType : Type
	{
		public override ulong Size
		{
			get
			{
				var result = 0UL;

				for (int i = 0; i < Fields.Length; i++)
				{
					result += Fields[i].Size;
				}

				return result;
			}
		}
	}
}
