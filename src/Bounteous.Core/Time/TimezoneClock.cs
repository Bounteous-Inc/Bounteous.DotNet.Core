using System;

namespace Bounteous.Core.Time;

public class TimeZoneClock : IClock
{
    private readonly TimeZoneInfo timeZoneInfo;

    public TimeZoneClock(TimeZoneInfo timeZoneInfo)
        => this.timeZoneInfo = timeZoneInfo;

    public DateTime UtcToday => ClockManager.Today.ToUniversalTime();

    public DateTime Now => TimeZoneInfo.ConvertTime(ClockManager.Now, timeZoneInfo);
    public DateTime NowUtc => ClockManager.Now.ToUniversalTime();
    public DateTime Today => TimeZoneInfo.ConvertTime(ClockManager.Now, timeZoneInfo).Date;

    public void Freeze()
        => ClockManager.Freeze();

    public void Freeze(DateTime timeToFreeze)
    {
        var frozenTime = !timeZoneInfo.Equals(TimeZoneInfo.Local)
            ? timeToFreeze.ToLocalTime()
            : timeToFreeze;

        ClockManager.Freeze(frozenTime);
    }

    public void Thaw()
        => ClockManager.Thaw();

    public DateTime NowToTimeZone(TimeZoneInfo info)
        => TimeZoneInfo.ConvertTime(Now, info);

    public DateTime TodayAt(TimezoneOffset offset)
        => offset.TodayFrom(NowUtc);

    public DateTime NowAt(TimezoneOffset offset)
        => offset.From(NowUtc);
}