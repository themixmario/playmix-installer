using System.Security.Cryptography;
using Microsoft.Maui.Storage;

namespace Apps2Samsung.Mobile.Services;

/// <summary>
/// Remembers which signing certificate each TV was last installed with (a SHA-256 of the author
/// certificate file), so the next install can say when it is about to use a different one: a TV
/// refuses an in-place update signed with another certificate.
/// </summary>
public static class CertificateFingerprint
{
	private static string Key(string tvIp) => "cert_fp_" + tvIp;

	private static string? Of(string authorP12Path)
	{
		try
		{
			using var sha = SHA256.Create();
			return Convert.ToHexString(sha.ComputeHash(File.ReadAllBytes(authorP12Path)));
		}
		catch (Exception ex)
		{
			System.Diagnostics.Trace.WriteLine($"[cert] Couldn't fingerprint the certificate: {ex.Message}");
			return null;
		}
	}

	/// <summary>True only when this TV has a remembered certificate and it differs from this one.</summary>
	public static bool Changed(string tvIp, string authorP12Path)
	{
		var saved = Preferences.Get(Key(tvIp), string.Empty);
		if (saved.Length == 0)
			return false;
		var current = Of(authorP12Path);
		return current is not null && !string.Equals(saved, current, StringComparison.Ordinal);
	}

	public static void Remember(string tvIp, string authorP12Path)
	{
		if (Of(authorP12Path) is { } fingerprint)
			Preferences.Set(Key(tvIp), fingerprint);
	}
}
