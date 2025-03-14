
using System.Data;
using Microsoft.Data.SqlClient;

namespace TaskMDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadDataGrid();
        }

        string connectionString = "server=localhost;database=RDP_desktop;integrated security=true;TrustServerCertificate=true";

        private void loadDataGrid()
        {
            string query = "select id ID, server_ip as 'SERVER IP', title AS 'TITLE' from creds";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    //// Customize the FillWeight for each column
                    //dataGridView1.Columns[0].FillWeight = 20; // 20% of the available space
                    //dataGridView1.Columns[1].FillWeight = 30; // 30% of the available space
                    //dataGridView1.Columns[2].FillWeight = 50; // 50% of the available space

                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "connBtn";
                    btn.HeaderText = "Action";
                    btn.Text = "Connect";
                    btn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(btn);
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ConnectRDP(string serverIp, string username, string password)
        {
            string recentDownloadFilePath = Path.Combine(@"C:\Users\Admin\Downloads", "rdp");
            string rootPath = Application.StartupPath;
            string rdpTempFile = Path.Combine(rootPath, "rdptemp", "rdptmp.txt");
            string rdpPath = Path.Combine(rootPath, "rdp", "rdp.rdp");

            //RDPConnect.RDPConnect.Connector(serverIp, username, password, rdpTempFile, rdpPath);
            Connect.RDPConnect.Connector(serverIp, username, password, rdpTempFile, rdpPath);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Get the IP address from the clicked row (column index 2 is server_ip)
                string ip_address = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString(); // server_ip is column 1

                // Ensure that the IP address is valid before proceeding
                if (!string.IsNullOrEmpty(ip_address))
                {
                    string query = $"SELECT username, password FROM creds WHERE server_ip = @server_ip";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            // Create and open the connection
                            connection.Open();

                            // Create the command and add the parameter to prevent SQL injection
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@server_ip", ip_address);

                                // Execute the query and read the results
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())  // Ensure there's data
                                    {
                                        string username = reader["username"]?.ToString();
                                        string password = reader["password"]?.ToString();

                                        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                                        {
                                            ConnectRDP(ip_address, username, password);
                                        }
                                        else
                                        {
                                            throw new Exception("Credentials not found.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No matching credentials found for the provided IP address.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid IP address.");
                }
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
