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
        private MySqlDataAdapter adapter = null;
        private DataTable table = null;

        public JournalForm()
        {
            InitializeComponent();
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            database = new DB();
            database.openConnection();
            adapter = new MySqlDataAdapter("select id_event,time_event,name_device,name_type from `events` JOIN `devices` ON events.id_device = devices.id_device JOIN `event_types` ON events.id_type = event_types.id_type; ", database.getConnection());
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            database.closeConnection();
        }
    }
}
