using DailyRhythms.Models;

namespace DailyRhythms
{
	public static class Utilities
	{
		public static DateTime ConvertUtcToLocalTime(string timeZoneId)
		{
			var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			var userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, userTimeZone);
			return userLocalTime;
		}
	}
}
