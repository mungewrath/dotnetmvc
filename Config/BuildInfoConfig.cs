
using System;

public class BuildInfoConfig {
    public string GitHash { get; set; }
    public string GitBranch { get; set; }

    // Unix-style timestamp
    public long _BuildDateRaw { get; set; }
    public DateTime BuildDate { get {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long unixTimeStampInTicks = (long) (_BuildDateRaw * TimeSpan.TicksPerSecond);
        return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
    } }
}