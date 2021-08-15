using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using rozetka_desk.Properties;
using MySql.Data.MySqlClient;

namespace rozetka_desk
{
    public partial class MainForm : Form
    {
        private UdpClient Client = new UdpClient(8081);
        private string ip = "127.0.0.1";
        private const int port = 8081;
        private int seconds = 0;
        private MySqlCommand command1 = null;
        private MySqlCommand command2 = null;
        private DB database = new DB();

        public MainForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Client.BeginReceive(new AsyncCallback(Recieve), null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Recieve(IAsyncResult res)
        {

            database.openConnection();
            command1 = new MySqlCommand("INSERT INTO `events` (`id_event`, `id_device`, `time_event`, `id_type`, `amperage`) VALUES (NULL, '1', @datetime, @type, @amper);", database.getConnection());
            command2 = new MySqlCommand("INSERT INTO `data` (`id_data`, `date_data`, `value_data`) VALUES (NULL, @date, @value);", database.getConnection());
            bool isOn = false;
            float data_float_amper = 0;
            float amperage_sum = 0;
            int device_on_data_recieved = 0;
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderendPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderendPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                this.Invoke(new MethodInvoker(delegate
                {
                    try
                    {
                        data_float_amper = float.Parse(data.ToString(), CultureInfo.InvariantCulture.NumberFormat) * 10;
                    }
                    catch { };

                    label2.Text = "Сила тока: " + data_float_amper.ToString() + " A";
                    this.chart1.Series[0].Points.AddXY(seconds, data_float_amper);
                    command2.Parameters.Add("@date", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    command2.Parameters.Add("@value", MySqlDbType.Float).Value = data_float_amper;
                    command2.ExecuteNonQuery();
                    if (isOn)
                    {
                        amperage_sum += data_float_amper;
                        device_on_data_recieved++;
                    }

                    //Включение прибора
                    if (data_float_amper >= 1 && isOn == false)
                    {
                        isOn = true;
                        command1.Parameters.Add("@datetime", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        command1.Parameters.Add("@type", MySqlDbType.Int32).Value = 1;
                        command1.Parameters.Add("@amper", MySqlDbType.Float).Value = null;
                        command1.ExecuteNonQuery();
                        pictureBox1.BackgroundImage = Resources.On;
                    }

                    //Отключение прибора
                    else if (data_float_amper < 1 && isOn == true)
                    {
                        isOn = false;
                        command1.Parameters.Add("@datetime", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        command1.Parameters.Add("@type", MySqlDbType.Int32).Value = 2;
                        command1.Parameters.Add("@amper", MySqlDbType.Float).Value = amperage_sum / device_on_data_recieved;
                        amperage_sum = 0;
                        device_on_data_recieved = 0;
                        command1.ExecuteNonQuery();
                        pictureBox1.BackgroundImage = Resources.Off;
                    }
                    command1.Parameters.Clear();
                    command2.Parameters.Clear();
                }));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
        }

        private void journal_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            JournalForm journalForm = new JournalForm();
            journalForm.Show();
        }

        private void adding_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddingDeviceForm addingDeviceForm = new AddingDeviceForm();
            addingDeviceForm.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
