using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashTuto
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pc\OneDrive\Desktop\CarWashTuto\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string Username = "";
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl where EName='"+UNameTb.Text+"' and EPass='"+PasswordTb.Text+"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                Username = UNameTb.Text;
                Washs Obj = new Washs();
                Obj.Show();
                this.Hide();
                Con.Close();
            }else
            {
                MessageBox.Show("Wrong UserName Or Password");
            }
            Con.Close();
        }
    }
}
