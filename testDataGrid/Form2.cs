using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testDataGrid
{
    public partial class Form2 : Form
    {
        Client Client { get; set; }
        public String rtnVal { get; set; }
        int test = 1;

        public event EventHandler NotButtonClicked;
        protected virtual void OnNotButtonClicked(EventArgs e)
        {
            var handler = NotButtonClicked;
            if (handler != null)
                handler(this, e);
        }

        public void MessageReceived(object sender, EventArgs e)
        {
            textBox1.Text = "Message received from form 1";
        }

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Client client)
        {
            this.Client = client;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Client.fname;
            this.rtnVal = Client.fname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //test = ~test;
            //textBox1.Text = test.ToString();
            OnNotButtonClicked(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            rtnVal = textBox1.Text;
        }
    }
}
