using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace rozetka_desk
{
    public partial class JournalForm : Form
    {
        private DB database = null;
        private MySqlDataAdapter adapter1 = null, adapter2 = null;
        private DataTable table = null, devices = null;

        public JournalForm()
        {
            InitializeComponent();
        }

        private void back_journal_button_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            database = new DB();
            database.openConnection();
            adapter1 = new MySqlDataAdapter("select id_event,name_device,time_event,name_type from `events` JOIN `devices` ON events.id_device = devices.id_device JOIN `event_types` ON events.id_type = event_types.id_type; ", database.getConnection());
            adapter2 = new MySqlDataAdapter("select name_device from `devices`", database.getConnection());
            database.closeConnection();
            table = new DataTable();
            devices = new DataTable();
            adapter1.Fill(table);
            adapter2.Fill(devices);
            comboBox1.Items.Clear();
            for (int i = 0; i < devices.Rows.Count; i++)
                comboBox1.Items.Add(devices.Rows[i]["name_device"]);
            dataGridView1.DataSource = table;
        }

        private void JournalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string device = comboBox1.Text;
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"name_device = '" + device + "'";
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            database = new DB();
            database.openConnection();
            adapter1 = new MySqlDataAdapter("select id_event,name_device,time_event,name_type from `events` JOIN `devices` ON events.id_device = devices.id_device JOIN `event_types` ON events.id_type = event_types.id_type; ", database.getConnection());
            adapter2 = new MySqlDataAdapter("select name_device from `devices`", database.getConnection());
            database.closeConnection();
            table = new DataTable();
            devices = new DataTable();
            adapter1.Fill(table);
            adapter2.Fill(devices);
            for (int i = 0; i < devices.Rows.Count; i++)
                comboBox1.Items.Add(devices.Rows[i]["name_device"]);
            dataGridView1.DataSource = table;
            
        }
    }
}
