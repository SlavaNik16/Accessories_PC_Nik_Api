using Accessories_PC_Nik.Common;

namespace Accessories_PC_Nik.Api.Infrastructures
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
