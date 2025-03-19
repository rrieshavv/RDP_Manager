using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TaskMDesktop.Library
{
    public static class PasswordManager
    {
        public static void SetNewPassword()
        {
            string jsonFilePath = GetJsonFilePath();
            string csvFilePath = GetCsvFilePath();

            string newPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Set your password:", "First-time Setup", "");

            if (!string.IsNullOrEmpty(newPassword))
            {
                string jsonDirectory = Path.GetDirectoryName(jsonFilePath)!;
                string csvDirectory = Path.GetDirectoryName(csvFilePath)!;

                Directory.CreateDirectory(jsonDirectory);
                Directory.CreateDirectory(csvDirectory);

                string hashedPassword = HashPassword(newPassword);

                var passwordData = new { Password = hashedPassword };

                string json = JsonSerializer.Serialize(passwordData);

                File.WriteAllText(jsonFilePath, json);

                if (!File.Exists(csvFilePath))
                {
                    File.WriteAllText(csvFilePath, "ID,SERVER IP,TITLE,USERNAME,PASSWORD\n");
                }

                MessageBox.Show($"Password has been set successfully, and an empty CSV file has been created at {csvFilePath}");
            }
            else
            {
                MessageBox.Show("Password cannot be empty. Please restart the application.");
                Application.Exit();
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool ValidatePassword(string enteredPassword)
        {
            if (!File.Exists(GetJsonFilePath()))
                return false;

            string json = File.ReadAllText(GetJsonFilePath());
            var storedData = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (storedData != null && storedData.ContainsKey("Password"))
            {
                return VerifyPassword(enteredPassword, storedData["Password"]);
            }

            return false;
        }

        private static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return HashPassword(enteredPassword) == storedHash;
        }


        public static string GetJsonFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RDPManager", "password.json");
        }

        public static string GetCsvFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RDPManager", "ip.csv");
        }
    }
}
