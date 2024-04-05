namespace Clinic.Models;

public static class Utils
{
	public static string ToTitle(string text)
	{
		return string.Concat(text[0].ToString().ToUpper(), text[1..].ToLower());
	}
}