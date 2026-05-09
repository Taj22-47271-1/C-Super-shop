using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperShopManagement
{
    public class LoginForm : Form
    {
        TextBox txtUsername;
        TextBox txtPassword;
        Button btnLogin;

        public LoginForm()
        {
            Text = "Login";
            Size = new Size(400, 300);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label()
            {
                Text = "SUPER SHOP LOGIN",
                Location = new Point(0, 20),
                Size = new Size(400, 40),
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblUser = new Label()
            {
                Text = "Username",
                Location = new Point(60, 90),
                Size = new Size(100, 30)
            };

            txtUsername = new TextBox()
            {
                Location = new Point(170, 90),
                Size = new Size(150, 30)
            };

            Label lblPass = new Label()
            {
                Text = "Password",
                Location = new Point(60, 135),
                Size = new Size(100, 30)
            };

            txtPassword = new TextBox()
            {
                Location = new Point(170, 135),
                Size = new Size(150, 30),
                PasswordChar = '*'
            };

            btnLogin = new Button()
            {
                Text = "Login",
                Location = new Point(170, 185),
                Size = new Size(100, 35),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White
            };

            btnLogin.Click += BtnLogin_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblUser);
            Controls.Add(txtUsername);
            Controls.Add(lblPass);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string role = DatabaseHelper.Login(txtUsername.Text, txtPassword.Text);

            if (role == "Admin")
            {
                BillingForm billing = new BillingForm("Admin");
                billing.Show();
                this.Hide();
            }
            else if (role == "Employee")
            {
                BillingForm billing = new BillingForm("Employee");
                billing.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }
        }
    }
}