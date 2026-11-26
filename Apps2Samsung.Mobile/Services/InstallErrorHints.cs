using Apps2Samsung.Mobile.Localization;

namespace Apps2Samsung.Mobile.Services;

/// <summary>
/// Puts what to do next in front of a raw install failure: the usual causes (TV off or unreachable,
/// a certificate that does not match, no room on the TV) get a plain sentence with the fix, and the
/// original text stays below it for anyone debugging. An unknown failure is left as it was.
/// </summary>
public static class InstallErrorHints
{
	public static string Compose(Exception ex)
	{
		var raw = string.Format(L10n.Get("statusInstallFailed"), ex.Message);
		return HintKey(ex) is { } key ? L10n.Get(key) + "\n\n" + raw : raw;
	}

	private static string? HintKey(Exception ex)
	{
		var text = string.Join(" ", ExceptionTexts(ex)).ToLowerInvariant();

		if (text.Contains("refused") || text.Contains("timed out") || text.Contains("timeout")
			|| text.Contains("unreachable") || text.Contains("no route") || text.Contains("host is down")
			|| text.Contains("network is") || text.Contains("connection reset"))
			return "errHintTvUnreachable";

		if (text.Contains("no space") || text.Contains("not enough space") || text.Contains("insufficient")
			|| text.Contains("disk full") || text.Contains("out of space"))
			return "errHintNoSpace";

		if (text.Contains("certificate") || text.Contains("signature") || text.Contains("author")
			|| text.Contains("not valid yet") || text.Contains("distributor"))
			return "errHintCertificate";

		return null;
	}

	private static IEnumerable<string> ExceptionTexts(Exception ex)
	{
		for (Exception? e = ex; e is not null; e = e.InnerException)
			yield return e.Message ?? string.Empty;
	}
}
