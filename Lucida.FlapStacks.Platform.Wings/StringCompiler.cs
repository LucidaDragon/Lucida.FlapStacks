using System;
using System.Globalization;
using System.Text;

namespace Lucida.FlapStacks.Platform.Wings
{
	public class StringCompiler : EmitSource
	{
		public override string Name => "wings";

		private string[] Lines = new string[0];

		public override void EmitTo(Emitter emitter)
		{
			var rows = ParseRows(Lines);
			var instructions = new Instruction[rows.Length];

			for (int i = 0; i < rows.Length; i++)
			{
				var row = rows[i];
				var inst = ParseInstruction(row);

				if (inst is null) throw new Exception($"Invalid instruction \"{row.Keyword} {string.Join(", ", row.Arguments)}\"");

				instructions[i] = inst;
			}

			for (int i = 0; i < instructions.Length; i++)
			{
				instructions[i].Emit(emitter);
			}
		}

		public override void Load(Stream stream)
		{
			var buffer = new List<byte>();

			while (stream.CanRead)
			{
				buffer.Add(stream.ReadByte());
			}

			var data = buffer.ToArray();

			Lines = Encoding.UTF8.GetString(data, 0, data.Length).Split('\n');
		}

		private static Instruction ParseInstruction(Row row)
		{
			var args = ParseArgs(row.Arguments);

			if (args == null) return null;

			for (int i = 0; i < Instruction.Instructions.Length; i++)
			{
				var type = Instruction.Instructions[i];

				if (type.IsValid(row.Keyword, args.Length))
				{
					return type.Create(row.Keyword, args);
				}
			}

			return null;
		}

		private static Value[] ParseArgs(string[] args)
		{
			var result = new Value[args.Length];

			for (int i = 0; i < result.Length; i++)
			{
				var arg = args[i];

				if (arg.StartsWith("0x") && ulong.TryParse(arg.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ulong argValue))
				{
					result[i] = new Constant(argValue);
				}
				else if (long.TryParse(arg, out long signedArg))
				{
					result[i] = new Constant((ulong)signedArg);
				}
				else if (ulong.TryParse(arg, out ulong unsignedArg))
				{
					result[i] = new Constant(unsignedArg);
				}
				else
				{
					return null;
				}
			}

			return result;
		}

		private static Row[] ParseRows(string[] lines)
		{
			var result = new List<Row>();

			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i].Trim();

				var commentIndex = line.IndexOf("//");

				if (commentIndex >= 0)
				{
					line = line.Substring(commentIndex);
				}

				if (line.Length > 0)
				{
					var parts = line.Replace(",", " ").Replace("  ", " ").Split(' ');

					if (parts.Length > 0)
					{
						var args = new string[parts.Length - 1];

						for (int j = 0; j < args.Length; j++)
						{
							args[j] = parts[j + 1];
						}

						result.Add(new Row(parts[0], args));
					}
				}
			}

			return result.ToArray();
		}

		private struct Row
		{
			public string Keyword;
			public string[] Arguments;

			public Row(string keyword, string[] arguments)
			{
				Keyword = keyword;
				Arguments = arguments;
			}
		}
	}
}
