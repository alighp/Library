using System;

namespace Library.Infrastructure
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }

    public class UtcDateTimeService : IDateTimeService
    {
        public DateTime Today => DateTime.Now.Date.ToUniversalTime();

        public DateTime Now => DateTime.Now.ToUniversalTime();
    }
}
