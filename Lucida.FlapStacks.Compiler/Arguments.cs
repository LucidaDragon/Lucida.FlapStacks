using Lucida.FlapStacks.Compiler.Args;

namespace Lucida.FlapStacks.Compiler
{
	public static class Arguments
	{
		public static readonly ArgHandler[] Handlers = new ArgHandler[]
		{
			new DeviceArg(),
			new EmitterArg(),
			new HelpArg(),
			new IncludeArg(),
			new NugetArg(),
			new OutputArg(),
			new ParserArg(),
			new PluginArg(),
			new SourceArg(),
			new TargetArg(),
			new PreArg(),
			new PostArg()
		};
	}
}
