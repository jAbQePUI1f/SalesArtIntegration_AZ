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
            var response = await LoginManager.LoginAsync(txtboxUserName.Text, txtBoxPassword.Text);
            if (!response.State)
            {
                MessageBox.Show(response.Messages.GetMessages(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UserSharedInfo.UserInfo.UserName = txtboxUserName.Text;
            UserSharedInfo.UserInfo.Password = txtBoxPassword.Text;
            UserSharedInfo.UserInfo.Token = response.Token;
            // Beni hatýrla seçiliyse kullanýcý bilgilerini kaydet
            if (rememberMeBox.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUserName = txtboxUserName.Text;
                Properties.Settings.Default.SavedPassword = txtBoxPassword.Text;
            }
            else // Deðilse temizle
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUserName = "";
                Properties.Settings.Default.SavedPassword = "";
            }
            Properties.Settings.Default.Save();
            new DataIntegrationForm().Show();
            //new SplashScreen().Show();
            this.Hide();

        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            // Kaydedilmiþ bilgiler varsa otomatik doldur
            if (Properties.Settings.Default.RememberMe)
            {
                txtboxUserName.Text = Properties.Settings.Default.SavedUserName;
                txtBoxPassword.Text = Properties.Settings.Default.SavedPassword;
                rememberMeBox.Checked = true;
            }
            else
            {
                rememberMeBox.Checked = false;
            }
        }
    }
}

