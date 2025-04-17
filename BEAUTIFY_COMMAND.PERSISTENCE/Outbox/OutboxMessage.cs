namespace BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
public class OutboxMessage
{
    private static readonly DateTimeOffset VietNameTimeZone =
        TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

    public Guid Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset OccurredOnUtc { get; set; } = VietNameTimeZone;

    public DateTimeOffset? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}