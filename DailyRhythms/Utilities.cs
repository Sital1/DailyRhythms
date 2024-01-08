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

		public static DateTime AddTimeToUtc(DateTime utcTime, string timeZoneId)
		{
			// Find the time zone
			TimeZoneInfo timeZoneInfo;
			try
			{
				timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			}
			catch (TimeZoneNotFoundException)
			{
				throw new ArgumentException($"Invalid time zone ID: {timeZoneId}");
			}

			// Convert the UTC time to the local time in the specified time zone
			DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);



			return localTime;
		}
	}
}
