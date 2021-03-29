namespace Lucida.FlapStacks.CodeDOM.Types
{
	public class ArrayType : ComplexType
	{
		public override Type[] Fields
		{
			get
			{
				var length = Length.Get();
				var result = new Type[length];
				for (ulong i = 0; i < length; i++)
				{
					result[i] = ElementType;
				}
				return result;
			}

			set { }
		}

		public Type ElementType { get; set; }
		public Value Length { get; set; }

		public ArrayType(Type elementType, Value length)
		{
			ElementType = elementType;
			Length = length;
		}
	}
}
