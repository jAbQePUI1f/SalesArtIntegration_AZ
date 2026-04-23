public class LogDialog : Form
{
    private TextBox logTextBox;

    public LogDialog(string logContent, string title = "İşlem Raporu")
    {
        InitializeComponent(logContent, title);
    }

    private void InitializeComponent(string logContent, string title)
    {
        this.Text = title;
        this.Size = new Size(650, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // TextBox - Seçilebilir, kopyalanabilir
        logTextBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Text = logContent,
            Font = new Font("Consolas", 9F), // Monospace font
            ScrollBars = ScrollBars.Vertical,
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            BorderStyle = BorderStyle.None,
            Padding = new Padding(10)
        };

        // Alt panel - Butonlar için
        var bottomPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            Padding = new Padding(10)
        };

        // Kopyala butonu
        var btnCopy = new Button
        {
            Text = "Tümünü Kopyala",
            Size = new Size(120, 30),
            Location = new Point(10, 10),
            FlatStyle = FlatStyle.System
        };
        btnCopy.Click += (s, e) =>
        {
            Clipboard.SetText(logTextBox.Text);
            MessageBox.Show("Rapor panoya kopyalandı!", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        // Kapat butonu
        var btnClose = new Button
        {
            Text = "Kapat",
            Size = new Size(100, 30),
            Location = new Point(140, 10),
            DialogResult = DialogResult.OK,
            FlatStyle = FlatStyle.System
        };

        bottomPanel.Controls.AddRange(new Control[] { btnCopy, btnClose });

        this.Controls.Add(logTextBox);
        this.Controls.Add(bottomPanel);
        this.AcceptButton = btnClose;
    }

    public static void Show(string logContent, string title = "İşlem Raporu")
    {
        using (var dialog = new LogDialog(logContent, title))
        {
            dialog.ShowDialog();
        }
    }
}