using System.Text.RegularExpressions;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Payments;
public static partial class QrContentParser
{
    private static Guid GuidParser(string guidString)
    {
        if (guidString.Length != 32)
            throw new FormatException("Invalid GUID format. GUID should be 32 characters long.");

        // Insert hyphens to match the Guid format (8-4-4-4-12)
        var formattedOrderId =
            $"{guidString[..8]}-{guidString.Substring(8, 4)}-{guidString.Substring(12, 4)}-{guidString.Substring(16, 4)}-{guidString[20..]}";

        // Parse the string back into a Guid
        return Guid.Parse(formattedOrderId);
    }

    private static QrParseResult ParseQrContent(string content)
    {
        // Convert content to lowercase first
        var lowercaseContent = content.ToLower();

        // Pattern now uses lowercase 'antree' since we've converted the content
        const string pattern = @"beautify(order|sub)([a-f0-9]{32})";

        var match = MyRegex().Match(lowercaseContent);

        if (!match.Success) throw new FormatException("Could not find valid Antree transaction in content");

        return new QrParseResult
        {
            TransactionType = match.Groups[1].Value.ToUpper(), // Convert type back to uppercase for consistency
            TransactionId = match.Groups[2].Value // Keep ID as found (lowercase)
        };
    }

    public static (string type, Guid id) TakeOrderIdFromContent(string content)
    {
        var parseResult = ParseQrContent(content);

        // Validate if the extracted ID is a valid GUID (32 characters)
        if (parseResult.TransactionId.Length != 32)
            throw new FormatException("Transaction ID must be 32 characters long.");

        return (parseResult.TransactionType, GuidParser(parseResult.TransactionId));
    }

    [GeneratedRegex("beautify(order|sub)([a-f0-9]{32})")]
    private static partial Regex MyRegex();
}

public class QrParseResult
{
    public string TransactionType { get; set; }
    public string TransactionId { get; set; }
}