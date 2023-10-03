using Google.Protobuf.WellKnownTypes;

namespace Shared.Utils
{
    public static class GRPCUtils
    {
        public static DateTime ConvertDate(Timestamp? timestamp)
        {
            return timestamp?.ToDateTime() ?? DateTime.MinValue;
        }

        public static Timestamp ConvertDate(DateTime? dateTime)
        {
            if (dateTime == null) return Timestamp.FromDateTime(DateTime.MinValue);

            var dtUtc = DateTime.SpecifyKind((DateTime)dateTime, DateTimeKind.Utc);
            return Timestamp.FromDateTime(dtUtc);
        }
    }
}
