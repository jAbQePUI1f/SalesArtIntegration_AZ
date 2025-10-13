using System.Threading;

namespace SalesArtIntegration_AZ
{
    internal static class Program
    {
        private static Mutex mutex;
        [STAThread]
        static void Main()
        {
            bool AcikUygulamaVar = false;
            mutex = new Mutex(true, "PaketServis", out AcikUygulamaVar);
            if (AcikUygulamaVar)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;
                Application.Run(new loginForm());
            }
            else
            {
                MessageBox.Show("SalesArt Integrator zaten çalýþýyor!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
        }
    }
}