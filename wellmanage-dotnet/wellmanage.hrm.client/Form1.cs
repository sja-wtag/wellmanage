using wellmanage.application.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace wellmanage.hrm.client
{
    public partial class Form1 : Form
    {
        private readonly IUserService _userService;
        private event Action<bool> OnAuthenticationEvent;
        public Form1(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
            OnAuthenticationEvent += OnAuthenticationResponse;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            AuthenticateAdmin(email, password);
        }

        private async Task AuthenticateAdmin(string email , string password)
        {
            bool isAuthenticated = await _userService.AuthenticateAdmin(email, password);
            OnAuthenticationEvent.Invoke(isAuthenticated);
        }

        private void OnAuthenticationResponse(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                HomeForm homeForm = new HomeForm(_userService);
                homeForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
