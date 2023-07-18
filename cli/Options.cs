using CommandLine;

namespace cli;
public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input file")]
    public string Input { get; set; }

}
