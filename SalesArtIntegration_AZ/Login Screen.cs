namespace SalesArtIntegration_AZ
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bttnLogin_Click(object sender, EventArgs e)
        {
            loginForm login = new loginForm();
            if (txtboxUserName.Text == "admin" && txtBoxPassword.Text == "1234")
            {
                SplashScreen splashScreen = new SplashScreen();
                splashScreen.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Kullanýcý adý veya þifre yanlýþ!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
