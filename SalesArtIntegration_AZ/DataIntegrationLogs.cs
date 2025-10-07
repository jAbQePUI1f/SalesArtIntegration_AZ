namespace SalesArtIntegration_AZ
{
    public partial class DataIntegrationLogs : Form
    {
        public DataIntegrationLogs()
        {
            InitializeComponent();
        }

        private void loqLabel_Click(object sender, EventArgs e)
        {

        }
        private void logListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                string logPath = Path.Combine(Application.StartupPath, "logfile.txt");

                try
                {
                    if (File.Exists(logPath))
                    {
                        File.WriteAllText(logPath, string.Empty); // Log dosyasını temizle

                        // DataSource’u sıfırla ve yeniden ata
                        logListBox.DataSource = null;
                        logListBox.DataSource = new List<string>(); // Boş bir liste ata

                        MessageBox.Show("Log başarıyla temizlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Log dosyası bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Log temizlenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataIntegrationLogs_Load(object sender, EventArgs e)
        {
            string line = "";
            try
            {
                List<string> logs = new List<string>();
                using (StreamReader inputFile = new StreamReader("logfile.txt"))
                {
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        string log = line;
                        logs.Add(log);
                    }
                }

                List<string> logsReverse = new List<string>();
                for (int i = 1; i <= logs.Count; i++)  // son kayıt en başta görünmesi için yazıldı
                {
                    var asdf = logs[logs.Count - i].ToString();
                    logsReverse.Add(logs[logs.Count - i].ToString());
                }
                logListBox.DataSource = logsReverse;
            }
            catch
            {
                MessageBox.Show("Log Dosyası Bulunamadı", "Log Bilgisi", MessageBoxButtons.OK);
            }
        }
    }
}
