using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rozetka_desk
{
    public partial class AddingDeviceForm : Form
    {
        public AddingDeviceForm()
        {
            InitializeComponent();
        }

        private void adding_button_Click(object sender, EventArgs e)
        {
            string device_name = name_textBox.Text;
            string device_ip_test = ip_textBox.Text;
            string[] parts = device_ip_test.Split('.');
            if (parts.Length < 4)
            {
                MessageBox.Show("IP is not correct!");
                ip_textBox.Clear();
                return;
            }
            else
            {
                foreach (string part in parts)
                {
                    byte checkPart = 0;
                    if (!byte.TryParse(part, out checkPart))
                    {
                        MessageBox.Show("IP is not correct!");
                        ip_textBox.Clear();
                        return;
                    }
                }
                try
                {
                    MessageBox.Show("Device added!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
        }

        private void back_adding_button_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }

        private void AddingDeviceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }
    }
}
