using Microsoft.Extensions.CommandLineUtils;

namespace NWN.Xenomech.CLI
{
    internal class Program
    {
        private static readonly HakBuilder _hakBuilder = new();
        private static readonly ModulePacker _modulePacker = new();
        private static readonly AdHocTool _adHocTool = new();
        private static readonly DeployBuild _deployBuild = new();

        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            // Set up the options.
            var adHocToolOption = app.Option(
                "-$|-a |--adhoc",
                "Ad-hoc code testing.",
                CommandOptionType.NoValue);

            var hakBuilderOption = app.Option(
                "-$|-k |--hak",
                "Builds hakpak files based on the hakbuilder.json configuration file.",
                CommandOptionType.NoValue
            );

            var deployOption = app.Option(
                "-$|-o |--outputDeploy",
                "Deploys DLLs in the bin folder to the NWN dotnet directory.",
                CommandOptionType.NoValue
            );

            var modulePackerOption = app.Option(
                "-$|-p |--pack",
                "Packs a module at the specified path. Target must be the path to a .mod file.",
                CommandOptionType.SingleValue
            );

            var moduleUnpackOption = app.Option(
                "-$|-u |--unpack",
                "Unpacks a module within the running directory. Target must be the path to a .mod file.",
                CommandOptionType.SingleValue
            );

            app.HelpOption("-? | -h | --help");

            app.OnExecute(() =>
            {
                if (hakBuilderOption.HasValue())
                {
                    _hakBuilder.Process();
                }

                if (modulePackerOption.HasValue())
                {
                    _modulePacker.PackModule(modulePackerOption.Value());
                }

                if (moduleUnpackOption.HasValue())
                {
                    _modulePacker.UnpackModule(moduleUnpackOption.Value());
                }

                if (adHocToolOption.HasValue())
                {
                    _adHocTool.Process();
                }

                if (deployOption.HasValue())
                {
                    _deployBuild.Process();
                }

                return 0;
            });

            app.Execute(args);
        }
    }
}
