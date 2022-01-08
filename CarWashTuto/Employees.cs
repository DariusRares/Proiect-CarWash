using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CarWashTuto
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            displayEmp();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pc\OneDrive\Desktop\CarWashTuto\CarWashDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            ENameTb.Text = "";
            EAddTb.Text = "";
            EPhoneTb.Text = "";
            EGenCb.SelectedIndex = -1;
        }
        private void displayEmp()
        {
            Con.Open();
            string Query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(ENameTb.Text == "" || EAddTb.Text == "" || EGenCb.SelectedIndex == -1 || EPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EName,Ephone,EGen,EAdd,EPass) values(@En,@Ep,@Eg,@Ea,@Epa)", Con);
                    cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                    cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                    cmd.Parameters.AddWithValue("@Epa", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved");
                    
                    Con.Close();
                    displayEmp();
                    Reset();

                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ENameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(ENameTb.Text == "")
            {
                Key = 0;
            }else
            {
               Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(Key == 0)
            {
                MessageBox.Show("Select The Employee");
            }else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EId=@EmId", Con);
                cmd.Parameters.AddWithValue("@EmId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Deleted");
                Con.Close();
                displayEmp();
                Reset();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ENameTb.Text == "" || EAddTb.Text == "" || EGenCb.SelectedIndex == -1 || EPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EName=@En,EPhone=@Ep,EGen=@Eg,EAdd=@Ea where EId=@EmId", Con);
                cmd.Parameters.AddWithValue("@En", ENameTb.Text);
                cmd.Parameters.AddWithValue("@Ep", EPhoneTb.Text);
                cmd.Parameters.AddWithValue("@Eg", EGenCb.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Ea", EAddTb.Text);
                cmd.Parameters.AddWithValue("@EmId", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Updated");

                Con.Close();
                displayEmp();
                Reset();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
            Obj.Show();
            this.Hide();
        }
    }
}
