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

namespace ADO.NET_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data source=DESKTOP-IDSN6PJ\SQLEXPRESS01;initial catalog=GI; integrated security =true;");
        SqlCommand command;
        string queryString;
        SqlDataReader reader;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            queryString = "select count(*) from Etudiant";
            command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                int numberOfRows = (Int32)command.ExecuteScalar();
                if (numberOfRows == 0)
                {
                    btnDelete.Enabled = false;
                    btnDisplayAll.Enabled = false;
                    btnUpDate.Enabled = false;
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("an Error accured");
            }
        }
        private bool IsinputsValid()
        {
           
            if (textBoxCin.Text == "")
            {
                MessageBox.Show("Veuillez inserez votre CNE .");
                return false;
            }

          
            else if (textBoxFirstName.Text == "")
            {
     
                MessageBox.Show("Veuillez inserez votre Prenom");
                return false;
            }
            else if (textBoxLastName.Text == "")
            {
                MessageBox.Show("Veuillez inserez votre Nom");
                return false;
            }
            else
                return true;
        }

        private void btnUpDate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cliquez");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string message = "Etes-vous sur de continuer ";
            string title = "Fermez";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (IsinputsValid())
                {
                    queryString = "INSERT INTO Etudiant  VALUES(@CNE,@Penom,@Nom);";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@CNE", SqlDbType.VarChar);
                    command.Parameters["@CNE"].Value = textBoxCin.Text;
                    command.Parameters.Add("@Prenom", SqlDbType.VarChar);
                    command.Parameters["@Prenom"].Value = textBoxFirstName.Text;
                    command.Parameters.Add("@Nom", SqlDbType.VarChar);
                    command.Parameters["@Nom"].Value = textBoxLastName.Text;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        queryString = "select * from Etudiant";
                        command = new SqlCommand(queryString, connection);
                        reader = command.ExecuteReader();
                       
                        DataTable dataTable = new DataTable();

                        dataTable.Load(reader);

                        
                        this.dataGridView1.DataSource = dataTable;

                        
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Erreur ");
                    }
                }


            }


        }

        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void lblCin_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
