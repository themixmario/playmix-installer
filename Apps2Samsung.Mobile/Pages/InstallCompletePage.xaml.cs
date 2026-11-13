namespace Apps2Samsung.Mobile.Pages;

public partial class InstallCompletePage : ContentPage
{
	/// <param name="note">One more line under the message, such as why the app went in as a separate
	/// app (#1028); null for none.</param>
	public InstallCompletePage(string appName, string? note = null)
	{
		InitializeComponent();
		MessageLabel.Text = $"¡{appName} se instaló correctamente!";
		NoteLabel.Text = note;
		NoteLabel.IsVisible = !string.IsNullOrWhiteSpace(note);
	}

	private async void OnCloseClicked(object? sender, EventArgs e) =>
		await Navigation.PopModalAsync();
}
