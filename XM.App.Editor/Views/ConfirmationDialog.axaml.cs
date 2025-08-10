namespace XM.App.Editor.Views;

public partial class ConfirmationDialog : Window
{
    public ConfirmationDialog(string title, string message)
    {
        if (title is null) throw new System.ArgumentNullException(nameof(title));
        if (message is null) throw new System.ArgumentNullException(nameof(message));
        InitializeComponent();
        Title = title;

        if (this.FindControl<TextBlock>("MessageText") is { } messageText)
        {
            messageText.Text = message;
        }

        if (this.FindControl<Button>("OkButton") is { } ok)
        {
            ok.Click += (_, __) => Close(true);
        }

        if (this.FindControl<Button>("CancelButton") is { } cancel)
        {
            cancel.Click += (_, __) => Close(false);
        }
    }
}


