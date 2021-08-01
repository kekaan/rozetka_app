﻿using System;
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
    public partial class Form1 : Form
    {
        UdpClient Client = new UdpClient(8081);
        const string ip = "127.0.0.1";  //СМЕНИ НА СВОЙ IP
        const int port = 8081;
        int seconds = 0;

        public Form1()
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
                richTextBox_sensor_readings.Text += ex.Message.ToString();
            }
        }

        void Recieve(IAsyncResult res)
        {

            DB database = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `events` (`id_event`, `id_device`, `time_event`, `id_type`) VALUES (NULL, '1', @datetime, @type);", database.getConnection());
            bool isOn = false;
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
                    float data_float = float.Parse(data.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    richTextBox_sensor_readings.Text += data;
                    richTextBox_sensor_readings.Text += "\r\n";                
                    richTextBox_amperage.Text += data_float * 10;
                    richTextBox_amperage.Text += "\r\n";
                    richTextBox_sensor_readings.SelectionStart = richTextBox_sensor_readings.TextLength;
                    richTextBox_amperage.SelectionStart = richTextBox_sensor_readings.TextLength;
                    richTextBox_sensor_readings.ScrollToCaret();
                    richTextBox_amperage.ScrollToCaret();             
                    this.chart1.Series[0].Points.AddXY(seconds, data_float);
                    command.Parameters.Add("@datetime", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    //Включение прибора
                    if (data_float >= 0.1 && isOn == false)
                    {
                        isOn = true;
                        command.Parameters.Add("@type", MySqlDbType.Int32).Value = 1;
                        database.openConnection();
                        command.ExecuteNonQuery();
                        database.closeConnection();
                        pictureBox1.BackgroundImage = Resources.On;
                    }

                    //Отключение прибора
                    else if (data_float < 0.1 && isOn == true)
                    {
                        isOn = false;
                        command.Parameters.Add("@type", MySqlDbType.Int32).Value = 2;
                        database.openConnection();
                        command.ExecuteNonQuery();
                        database.closeConnection();
                        pictureBox1.BackgroundImage = Resources.Off;
                    }
                    command.Parameters.Clear();
                }));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
        }

    }
}
