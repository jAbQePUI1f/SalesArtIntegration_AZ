namespace SalesArtIntegration_AZ
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            bool AcikUygulamaVar = false;
            Mutex m = new Mutex(true, "PaketServis", out AcikUygulamaVar);
            if (AcikUygulamaVar)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new loginForm());
            }
            else
            {
                MessageBox.Show("SalesArt Integrator zaten çalýþýyor!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}