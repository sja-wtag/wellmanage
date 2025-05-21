namespace wellmanage.hrm.client
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private Label usernameLabel;
        private Label passwordLabel;
        private Button loginButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.emailTextBox = new TextBox();
            this.passwordTextBox = new TextBox();
            this.usernameLabel = new Label();
            this.passwordLabel = new Label();
            this.loginButton = new Button();

            this.SuspendLayout();


            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(30, 30);
            this.usernameLabel.Text = "Email:";

            this.emailTextBox.Location = new System.Drawing.Point(120, 30);
            this.emailTextBox.Width = 200;

            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(30, 70);
            this.passwordLabel.Text = "Password:";

            this.passwordTextBox.Location = new System.Drawing.Point(120, 70);
            this.passwordTextBox.Width = 200;
            this.passwordTextBox.PasswordChar = '*';

     
            this.loginButton.Location = new System.Drawing.Point(120, 110);
            this.loginButton.Text = "Login";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);

            this.ClientSize = new System.Drawing.Size(370, 170);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginButton);
            this.Text = "Well Manage Login";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
