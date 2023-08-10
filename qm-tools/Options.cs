using CommandLine;

namespace cli;
public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input path of the .b3dm")]
    public string Input { get; set; }
}
