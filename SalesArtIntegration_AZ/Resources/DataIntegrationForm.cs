namespace SalesArtIntegration_AZ.Resources
{
    public partial class DataIntegrationForm : Form
    {
        public DataIntegrationForm()
        {
            InitializeComponent();
        }
        private void bttnTransferToCustomer_Click_1(object sender, EventArgs e)
        {

        }
        private void DataIntegrationForm_Load(object sender, EventArgs e)
        {

        }
        private void anaMenüyeDönToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }
        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
