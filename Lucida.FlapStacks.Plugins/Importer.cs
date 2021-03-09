using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lucida.FlapStacks.Plugins
{
	public static class Importer
	{
		public static Plugin ImportDll(string filename)
		{
			var assembly = Assembly.LoadFile(Path.GetFullPath(filename));

			foreach (var type in assembly.GetTypes())
			{
				var constructor = type.GetConstructor(Type.EmptyTypes);

				if (constructor != null && !type.IsAbstract && typeof(ModuleDefinition).IsAssignableFrom(type))
				{
					var md = (ModuleDefinition)constructor.Invoke(null);

					return new Plugin(md, GetTypes<EmitSource>(assembly), GetTypes<Emitter>(assembly), GetTypes<Device>(assembly));
				}
			}

			throw new FormatException($"The specified assembly \"{filename}\" does not contain a valid module definition.");
		}

		private static IReadOnlyList<T> GetTypes<T>(Assembly assembly) where T : class
		{
			var result = new System.Collections.Generic.List<T>();

			foreach (var type in assembly.GetTypes())
			{
				if (!type.IsAbstract && typeof(T).IsAssignableFrom(type))
				{
					try
					{
						result.Add((T)Activator.CreateInstance(type));
					}
					catch { }
				}
			}

			return result;
		}
	}
}
