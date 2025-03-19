namespace TaskMDesktop
{
    partial class MainPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPanel));
            loginPanel = new Panel();
            ErrorText = new Label();
            label1 = new Label();
            loginButton = new Button();
            loginTextBox = new TextBox();
            listPanel = new Panel();
            logoutBtn = new Button();
            dataGridView1 = new DataGridView();
            loginPanel.SuspendLayout();
            listPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.Controls.Add(ErrorText);
            loginPanel.Controls.Add(label1);
            loginPanel.Controls.Add(loginButton);
            loginPanel.Controls.Add(loginTextBox);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Location = new Point(0, 0);
            loginPanel.Margin = new Padding(4);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(1649, 1019);
            loginPanel.TabIndex = 0;
            // 
            // ErrorText
            // 
            ErrorText.AutoSize = true;
            ErrorText.ForeColor = Color.Red;
            ErrorText.Location = new Point(482, 344);
            ErrorText.Name = "ErrorText";
            ErrorText.Size = new Size(277, 30);
            ErrorText.TabIndex = 3;
            ErrorText.Text = "Error Message goes here ...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(482, 258);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(211, 30);
            label1.TabIndex = 2;
            label1.Text = "Enter your password";
            // 
            // loginButton
            // 
            loginButton.Location = new Point(852, 289);
            loginButton.Margin = new Padding(4);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(134, 41);
            loginButton.TabIndex = 1;
            loginButton.Text = "Log In";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += button1_Click;
            // 
            // loginTextBox
            // 
            loginTextBox.Location = new Point(482, 292);
            loginTextBox.Margin = new Padding(4);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.PasswordChar = '!';
            loginTextBox.Size = new Size(350, 37);
            loginTextBox.TabIndex = 0;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(logoutBtn);
            listPanel.Controls.Add(dataGridView1);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 0);
            listPanel.Margin = new Padding(4);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(1649, 1019);
            listPanel.TabIndex = 3;
            // 
            // logoutBtn
            // 
            logoutBtn.Location = new Point(1495, 70);
            logoutBtn.Name = "logoutBtn";
            logoutBtn.Size = new Size(142, 46);
            logoutBtn.TabIndex = 1;
            logoutBtn.Text = "Log Out";
            logoutBtn.UseVisualStyleBackColor = true;
            logoutBtn.Click += logoutBtn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 179);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1625, 828);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += openRdpDoubleClick;
            // 
            // NewForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1649, 1019);
            Controls.Add(listPanel);
            Controls.Add(loginPanel);
            Font = new Font("Segoe UI", 11F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(1671, 1075);
            Name = "NewForm";
            Text = "Task Manager";
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            listPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel loginPanel;
        private Label label1;
        private Button loginButton;
        private TextBox loginTextBox;
        private Panel listPanel;
        private Label ErrorText;
        private DataGridView dataGridView1;
        private Button logoutBtn;
    }
}