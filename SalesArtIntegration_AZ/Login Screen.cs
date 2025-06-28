using SalesArtIntegration_AZ.Manager.Helper;
using SalesArtIntegration_AZ.Manager.Login;
using SalesArtIntegration_AZ.Models;

namespace SalesArtIntegration_AZ
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private async void bttnLogin_Click(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(txtboxUserName.Text) || string.IsNullOrEmpty(txtBoxPassword.Text))
            //{
            //    MessageBox.Show("Kullanýcý bilgisi giriniz..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            var response = await LoginManager.LoginAsync("operasyon@safa.com", "Os1234");//"operasyon@safa.com", "Os1234"

            if (!response.State)
            {
                MessageBox.Show(response.Messages.GetMessages(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UserSharedInfo.UserInfo.UserName = txtboxUserName.Text;
            UserSharedInfo.UserInfo.Password = txtBoxPassword.Text;
            UserSharedInfo.UserInfo.Token = response.Token;

            new SplashScreen().Show();
            this.Hide();

            //loginForm login = new loginForm();
            //if (txtboxUserName.Text == "admin" && txtBoxPassword.Text == "1234")
            //{
            //    SplashScreen splashScreen = new SplashScreen();
            //    splashScreen.Show();
            //    this.Hide();

            //}
            //else
            //{
            //    MessageBox.Show("Kullanýcý adý veya þifre yanlýþ!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
