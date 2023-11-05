using System.CodeDom.Compiler;

if (args.Length != 3)
{
    throw new ArgumentException("There should be parsed 2 arguments to this program. The first should be a CSV file containing COSE algorithm descriptions, the second should be the destination to generate the C# file, and the third should be the namespace to use for the class.");
}

using FileStream sourceStream = File.Open(args[0], FileMode.Open);
using StreamWriter destinationStream = File.CreateText(args[1]);

using StreamReader reader = new(sourceStream);
using IndentedTextWriter writer = new(destinationStream);

// We skip the first line which is just the headers.
reader.ReadLine();

(string match, string replace)[] nameSanitationArguments = [
    ("-", "_"),
    (" w/ ", "_"),
    (" ", "_"),
    ("+", "and"),
    ("/", "_truncated_to_"),
];

writer.WriteLine($"namespace {args[2]};");
writer.WriteLine();
writer.WriteLine("public enum COSEAlgorithm : long");
writer.WriteLine("{");
writer.Indent++;
while (reader.ReadLine() is { } line)
{
    string[] lineSegments = line.Split(",");
    string name = nameSanitationArguments
        .Aggregate(lineSegments[0], (accu, sanitizer) => accu.Replace(sanitizer.match, sanitizer.replace));
    if (name is "Unassigned" || !int.TryParse(lineSegments[1], out int value))
    {
        continue;
    }
    string description = lineSegments[2];
    string capabilities = lineSegments[3];
    string changeController = lineSegments[4];
    string referenceList = lineSegments[5];
    string recommended = lineSegments[6];
    writer.WriteLine("/// <summary>");
    writer.WriteLine($"/// {description}");
    writer.WriteLine($"/// {(recommended == "Yes" ? "This is recommended to use." : "This is not recommended to use.")}");
    writer.WriteLine("/// </summary>");
    if (referenceList.Length >= 3)
    {
        writer.WriteLine("/// <remarks>");
        string[] references = referenceList[1..^1].Split("][");
        foreach(string reference in references)
        {
            writer.WriteLine($"/// <see href=\"https://www.iana.org/go/{reference.ToLower()}\">See the reference for {reference}</see>.");
        }
        writer.WriteLine("/// </remarks>");
    }
    writer.WriteLine($"{name} = {value},");
    writer.WriteLine();
}
writer.Indent--;
writer.WriteLine("}");
