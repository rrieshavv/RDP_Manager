using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using TaskMDesktop.Library;

namespace TaskMDesktop
{
    public partial class MainPanel : Form
    {
        private string jsonFilePath = PasswordManager.GetJsonFilePath();
        private string csvFilePath = PasswordManager.GetCsvFilePath();

        private DataTable credentialsTable;

        public MainPanel()
        {
            InitializeComponent();
            ErrorText.Text = "";

            // Check if password is set
            if (!File.Exists(jsonFilePath))
            {
                PasswordManager.SetNewPassword();
            }

            loginPanel.Visible = true;
            listPanel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = loginTextBox.Text;
            loginTextBox.Text = "";

            if (string.IsNullOrEmpty(password))
            {
                ErrorText.Text = "Please enter password.";
                return;
            }

            if (PasswordManager.ValidatePassword(password))
            {
                LoadDataGrid();
                loginPanel.Visible = false;
                listPanel.Visible = true;
            }
            else
            {
                ErrorText.Text = "Invalid password. Please try again.";
            }
        }

        private void searchTextBoxTextChange(object sender, EventArgs e)
        {
            DataView dv = credentialsTable.DefaultView;
            dv.RowFilter = string.Format("TITLE LIKE '%{0}%' OR [SERVER IP] LIKE '%{0}%' OR USERNAME LIKE '%{0}%'", searchTextBox.Text);
            dataGridView1.DataSource = dv.ToTable(false, "ID", "SERVER IP", "TITLE", "USERNAME"); // Exclude PASSWORD column from being shown
        }

        private void LoadDataGrid()
        {
            try
            {
                dataGridView1.Columns.Clear();
                credentialsTable = new DataTable();
                credentialsTable.Columns.Add("ID", typeof(int));
                credentialsTable.Columns.Add("SERVER IP", typeof(string));
                credentialsTable.Columns.Add("TITLE", typeof(string));
                credentialsTable.Columns.Add("USERNAME", typeof(string));
                credentialsTable.Columns.Add("PASSWORD", typeof(string));

                if (File.Exists(csvFilePath))
                {
                    var lines = File.ReadAllLines(csvFilePath);
                    foreach (var line in lines.Skip(1)) // Skipping the header row
                    {
                        var values = line.Split(',');
                        if (values.Length >= 5)
                        {
                            credentialsTable.Rows.Add(
                                int.Parse(values[0]), // ID
                                values[1], // Server IP
                                values[2], // Title
                                values[3], // Username
                                          values[4]  // Password
                            );
                        }
                    }
                }

                DataView view = new DataView(credentialsTable);
                dataGridView1.DataSource = view.ToTable(false, "ID", "SERVER IP", "TITLE", "USERNAME");

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Add Action Button
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "connBtn";
                btn.HeaderText = "Action";
                btn.Text = "Connect";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);

                // Apply Cell Formatting for Highlight & Bold
                dataGridView1.CellFormatting += (s, e) =>
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "SERVER IP" && e.Value != null)
                    {
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                        e.CellStyle.BackColor = Color.LightYellow; // Highlight color
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            listPanel.Visible = false;
        }

        private void ConnectRDP(string serverIp, string username, string password)
        {
            string recentDownloadFilePath = Path.Combine(@"C:\Users\Admin\Downloads", "rdp");
            string rootPath = Application.StartupPath;
            string rdpTempFile = Path.Combine(rootPath, "rdptemp", "rdptmp.txt");
            string rdpPath = Path.Combine(rootPath, "rdp", "rdp.rdp");

            Connect.RDPConnect.Connector(serverIp, username, password, rdpTempFile, rdpPath);
        }

        private void openRdpDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    // Get column index dynamically
                    int serverIpColumnIndex = dataGridView1.Columns["SERVER IP"].Index;

                    string? ip_address = dataGridView1.Rows[e.RowIndex].Cells[serverIpColumnIndex].Value?.ToString();

                    if (!string.IsNullOrEmpty(ip_address))
                    {
                        var row = credentialsTable.AsEnumerable()
                            .FirstOrDefault(r => r.Field<string>("SERVER IP") == ip_address);

                        if (row != null)
                        {
                            string? username = row.Field<string>("USERNAME");
                            string? password = row.Field<string>("PASSWORD");

                            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                            {
                                ConnectRDP(ip_address, username, password);
                            }
                            else
                            {
                                MessageBox.Show("Credentials not found.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No matching credentials found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid IP address.");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error connecting to RDP.");
            }
        }
    }
}
