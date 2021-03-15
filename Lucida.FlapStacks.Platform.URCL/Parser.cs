using Lucida.FlapStacks.Platform.URCL.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lucida.FlapStacks.Platform.URCL
{
	public class Parser : EmitSource
	{
		public override string Name => "urcl";

		private readonly Operand[] Operands;
		private readonly Instruction[] Instructions;

		private string[] Lines = new string[0];
		public readonly Dictionary<string, Label> Labels = new Dictionary<string, Label>();

		public Parser()
		{
			Operands = new Operand[]
			{
				new Immediate(),
				new LabelRef(this),
				new Port(),
				new Register()
			};

			Instructions = GetObjects<Instruction>();
		}

		public override void EmitTo(Emitter emitter)
		{
			var config = new UrclConfig();
			var rows = ParseRows(Lines);

			for (int i = 0; i < rows.Length; i++)
			{
				if (Label.TryParse(rows[i].Keyword, out string name))
				{
					try
					{
						Labels.Add(name, new Label(name, emitter));
					}
					catch (ArgumentException)
					{
						throw new Exception($"Duplicate label \"{name}\".");
					}
				}
			}

			var instructions = new Instruction[rows.Length];

			for (int i = 0; i < rows.Length; i++)
			{
				var row = rows[i];

				if (Label.TryParse(row.Keyword, out string name))
				{
					Labels[name].Mark(emitter);
				}
				else
				{
					var inst = ParseInstruction(config, row);

					if (inst is null) throw new Exception($"Invalid instruction \"{row.Keyword} {string.Join(", ", row.Operands)}\".");

					if (inst.MaxRegister > config.MaxRegisters.Value) config.MaxRegisters.Value = inst.MaxRegister;

					instructions[i] = inst;
				}
			}

			for (int i = 0; i < instructions.Length; i++)
			{
				instructions[i]?.Emit(config, emitter);
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

		private Instruction ParseInstruction(UrclConfig config, Row row)
		{
			var operands = ParseOperands(row.Operands);

			if (operands == null) return null;

			for (int i = 0; i < Instructions.Length; i++)
			{
				var type = Instructions[i];

				if (type.IsValid(row.Keyword, operands.Length))
				{
					return type.Create(config, row.Keyword, operands);
				}
			}

			return null;
		}

		private Operand[] ParseOperands(string[] operands)
		{
			var result = new Operand[operands.Length];

			for (int i = 0; i < result.Length; i++)
			{
				for (int j = 0; j < Operands.Length; j++)
				{
					if (Operands[j].TryParse(operands[i], out Operand operand))
					{
						result[i] = operand;
						break;
					}
				}

				if (result[i] == null) throw new Exception($"Invalid operand \"{operands[i]}\".");
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
					line = line.Substring(0, commentIndex).Trim();
				}

				if (line.Length > 0)
				{
					var parts = line.Replace(",", " ").Replace("  ", " ").Trim().Split(' ');

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

		private static T[] GetObjects<T>()
		{
			var result = new List<T>();

			var types = typeof(T).Assembly.GetTypes();

			for (int i = 0; i < types.Length; i++)
			{
				var type = types[i];

				if (typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
				{
					result.Add((T)Activator.CreateInstance(type));
				}
			}

			return result.ToArray();
		}

		private struct Row
		{
			public string Keyword;
			public string[] Operands;

			public Row(string keyword, string[] arguments)
			{
				Keyword = keyword;
				Operands = arguments;
			}
		}
	}
}
