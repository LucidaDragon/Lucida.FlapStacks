using System;

namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class Immediate : Operand
	{
		public override Value Value { get; }

		public Immediate() { }

		public Immediate(ulong value) : this(new Constant(value)) { }

		public Immediate(long value) : this(new Constant(value)) { }

		public Immediate(Value value)
		{
			Value = value;
		}

		public override string GetString()
		{
			return Value.Get().ToString();
		}

		public override void Pop(Emitter e)
		{
			throw new Exception("An immediate can not be the target of an operation.");
		}

		public override void Push(Emitter e)
		{
			e.Push(Value);
		}

		public override bool TryParse(string str, out Operand operand)
		{
			if (str.ToUpper().StartsWith("0X") && str.Length > 2 && TryParseHex(str.Substring(2), out ulong hexValue))
			{
				operand = new Immediate(hexValue);
				return true;
			}
			else if (str.ToUpper().StartsWith("0B") && str.Length > 2 && TryParseBinary(str.Substring(2), out ulong binValue))
			{
				operand = new Immediate(binValue);
				return true;
			}
			else if (str.StartsWith("'") && str.EndsWith("'") && str.Length > 2)
			{
				str = str.Substring(1, str.Length - 2);

				if (str.Length == 1)
				{
					operand = new Immediate(str[0]);
					return true;
				}
				else if (str.StartsWith("\\") && str.Length > 1)
				{
					if (str.Length == 2)
					{
						switch (str[1])
						{
							case 'a':
								operand = new Immediate('\a');
								break;
							case 'b':
								operand = new Immediate('\b');
								break;
							case 'e':
								operand = new Immediate(0x1B);
								break;
							case 'f':
								operand = new Immediate('\f');
								break;
							case 'n':
								operand = new Immediate('\n');
								break;
							case 'r':
								operand = new Immediate('\r');
								break;
							case 't':
								operand = new Immediate('\t');
								break;
							case 'v':
								operand = new Immediate('\v');
								break;
							default:
								operand = new Immediate(str[1]);
								break;
						}

						return true;
					}
					else if (str.Length > 2 && str[1] == 'u' && TryParseHex(str.Substring(2), out ulong charHex))
					{
						operand = new Immediate(charHex);
						return true;
					}
				}

				operand = null;
				return false;
			}
			else if (long.TryParse(str, out long signedValue))
			{
				operand = new Immediate(signedValue);
				return true;
			}
			else if (ulong.TryParse(str, out ulong unsignedValue))
			{
				operand = new Immediate(unsignedValue);
				return true;
			}
			else
			{
				operand = null;
				return false;
			}
		}

		private static bool TryParseHex(string str, out ulong result)
		{
			result = 0;

			str = str.ToUpper();

			if (str.Length > 64) return false;

			for (int i = 0; i < str.Length; i++)
			{
				var c = str[i];

				result <<= 4;

				if (c >= '0' && c <= '9')
				{
					result |= c - (ulong)'0';
				}
				else if (c >= 'A' && c <= 'F')
				{
					result |= (c - (ulong)'A') + 10;
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		private static bool TryParseBinary(string str, out ulong result)
		{
			result = 0;

			if (str.Length > 64) return false;

			for (int i = 0; i < str.Length; i++)
			{
				var c = str[i];

				result <<= 1;

				switch (c)
				{
					case '0':
						break;
					case '1':
						result |= 1;
						break;
					default:
						return false;
				}
			}

			return true;
		}
	}
}
