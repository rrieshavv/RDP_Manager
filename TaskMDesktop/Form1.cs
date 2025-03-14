
namespace TaskMDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIp = "10.21.146.222";
                string username = @"adserver\rihav";
                string password = "Cook@4062";
                string recentDownloadFilePath = Path.Combine(@"C:\Users\Admin\Downloads", "rdp");
                string rootPath = Application.StartupPath;
                string rdpTempFile = Path.Combine(rootPath, "rdptemp", "rdptmp.txt");
                string rdpPath = Path.Combine(rootPath, "rdp", "rdp.rdp");

               //RDPConnect.RDPConnect.Connector(serverIp, username, password, rdpTempFile, rdpPath);
                Connect.RDPConnect.Connector(serverIp, username, password, rdpTempFile, rdpPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
    }
}
