using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace testDataGrid
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private BindingSource source = new BindingSource();
        private ClientCollection clientCollection;
        private int ID = 0;

        Form2 f2;  

        // Our delegate (which "points" at any method which takes an object and EventArgs)
        // Look familiar? This is the signature of most control events on a form
        public delegate void SendMessage(object obj, EventArgs e);

        // Here is the event we trigger to send messages out to listeners
        public event SendMessage OnSendMessage;
        
        
        public Form1()
        {
            
            clientCollection = ClientCollection.GetJSON();
            f2 = new Form2(clientCollection.clientList.ElementAt(0));
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            SetupDataGridView();
            numericUpDown1.Maximum = clientCollection.clientList.Count;
            
        }

       

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Cells[1].Value = txt_FName.Text;
            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Cells[2].Value = txt_LName.Text;
            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Cells[3].Value = txt_Email.Text;
            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Cells[4].Value = txt_Height.Text;
            MessageBox.Show("Record Updated Successfully");
            ClearData();
            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Selected = false;
        }

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            if (this.clientsDataGridView.SelectedRows.Count > 0 )
            {
                this.clientsDataGridView.Rows.RemoveAt(
                    this.clientsDataGridView.SelectedRows[0].Index);
            }
            MessageBox.Show("Record Deleted Successfully");
            ClearData();
        }

        private void SetupDataGridView()
        {
            
            source.DataSource = clientCollection.clientList;
            
            clientsDataGridView.AutoGenerateColumns = false;
            //clientsDataGridView.AutoSize = true;
            clientsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            clientsDataGridView.DataSource = source;

            // Initialize and add a text box column.
            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "ID";
            column1.Name = "#";
            column1.ReadOnly = true;
            clientsDataGridView.Columns.Add(column1);

            // Initialize and add a text box column.
            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "fname";
            column2.Name = "First Name";
            clientsDataGridView.Columns.Add(column2);

            // Initialize and add a text box column.
            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "lname";
            column3.Name = "Last Name";
            clientsDataGridView.Columns.Add(column3);

            // Initialize and add a text box column.
            DataGridViewColumn column4 = new DataGridViewTextBoxColumn();
            column4.DataPropertyName = "email";
            column4.Name = "Email";
            clientsDataGridView.Columns.Add(column4);

            // Initialize and add a text box column.
            DataGridViewColumn column5 = new DataGridViewTextBoxColumn();
            column5.DataPropertyName = "height";
            column5.Name = "Height";
            clientsDataGridView.Columns.Add(column5);

            clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Selected = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DisplaySingleRow(int rowID)
        {
            ID = Convert.ToInt32(clientsDataGridView.Rows[rowID].Cells[0].Value.ToString());
            txt_FName.Text = clientsDataGridView.Rows[rowID].Cells[1].Value.ToString();
            txt_LName.Text = clientsDataGridView.Rows[rowID].Cells[2].Value.ToString();
            txt_Email.Text = clientsDataGridView.Rows[rowID].Cells[3].Value.ToString();
            txt_Height.Text = clientsDataGridView.Rows[rowID].Cells[4].Value.ToString();
        }
        private void clientsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(clientsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
           // clientsDataGridView.Rows[e.RowIndex].Selected = true;
            DisplaySingleRow(e.RowIndex);
        }

        private void ClearData()
        {
            txt_FName.Text = "";
            txt_LName.Text = "";
            txt_Email.Text = "";
            txt_Height.Text = "";
            ID = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Value changed to " + numericUpDown1.Value+"\nText is "+numericUpDown1.Text);
            DisplaySingleRow((int)numericUpDown1.Value-1);
            clientsDataGridView.Rows[(int)numericUpDown1.Value - 1].Selected = true;
        }

        private void clientsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(clientsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            DisplaySingleRow(e.RowIndex);
        }

        private void clientsDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && clientsDataGridView.SelectedRows.Count == 0 ) { 
                for (int i = 0; i < clientsDataGridView.Rows[e.RowIndex].Cells.Count; i++)
                {
                    clientsDataGridView[i, e.RowIndex].Style.BackColor = Color.Yellow;
                }
                DisplaySingleRow(e.RowIndex);
            }
        }

        private void clientsDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < clientsDataGridView.Rows[e.RowIndex].Cells.Count; i++)
                {
                    clientsDataGridView[i, e.RowIndex].Style.BackColor = Color.Empty;
                }
                if (clientsDataGridView.SelectedRows.Count == 0)
                {
                    ClearData();
                }
            }

        }

        private void clientsDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.IsEmpty)
            {
                clientsDataGridView.Rows[this.clientsDataGridView.SelectedRows[0].Index].Selected = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.hide();
            f2.NotButtonClicked += F2_NotButtonClicked;
            OnSendMessage += f2.MessageReceived;
            f2.Show();
            //textBox1.Text = f2.rtnVal;
            //f2.Close();
        }

        private void F2_NotButtonClicked(object sender, EventArgs e)
        {
            textBox1.Text = f2.rtnVal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // When we click the message button, tell others to respond to our button being clicked
            if (OnSendMessage != null)
            {
                OnSendMessage(this, e);
            }
        }
    }
}
