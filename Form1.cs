using Ethernet;
using System.Configuration;
using System.IO.Packaging;
using System.Net.Sockets;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer checkConnectionTimer = new System.Windows.Forms.Timer();
        private EthernetPacketSender _sender = new EthernetPacketSender();
        byte[] return_val;
        byte data;
        public Form1()
        {
            InitializeComponent();
            checkConnectionTimer.Interval = 1000; // Check every second
            checkConnectionTimer.Tick += new EventHandler(CheckConnectionStatus);
            checkConnectionTimer.Start();
        }
        private void CheckConnectionStatus(object sender, EventArgs e)
        {
            // Check if the connection is still active
            bool isConnected = _sender.IsConnected();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        int data_int;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int intValue;
            if (int.TryParse(textBox1.Text, out intValue)) 
            { 
                data_int = Convert.ToInt16(textBox1.Text);
                data = (byte)data_int;
            }
            else
            {
                this.textBox2.AppendText("The message should be an integer between 0 to 255! \r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (data_int > 255 || data_int < 0)
            {
                this.textBox2.AppendText("The message should be between 0 to 255! \r\n");
            }
            else
            {
                return_val = _sender.GetPacket(0x10, data);
                this.textBox2.AppendText("the resulting package is: ");

                for (int i = 0; i < return_val.Length; i++)
                {
                    this.textBox2.AppendText("0x" + return_val[i].ToString("X2") + " ");
                }
                this.textBox2.AppendText("\r\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (data_int > 255 || data_int < 0)
            {
                this.textBox2.AppendText("The message should be between 0 to 255! \r\n");
            }
            else
            {
                return_val = _sender.GetPacket(0x11, data);
                this.textBox2.AppendText("the resulting package is: ");

                for (int i = 0; i < return_val.Length; i++)
                {
                    this.textBox2.AppendText("0x" + return_val[i].ToString("X2") + " ");
                }
                this.textBox2.AppendText("\r\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (data_int > 255 || data_int < 0)
            {
                this.textBox2.AppendText("The message should be between 0 to 255! \r\n");
            }
            else
            {
                return_val = _sender.GetPacket(0x12, data);
                this.textBox2.AppendText("the resulting package is: ");

                for (int i = 0; i < return_val.Length; i++)
                {
                    this.textBox2.AppendText("0x" + return_val[i].ToString("X2") + " ");
                }
                this.textBox2.AppendText("\r\n");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Int32 port = 0;
            string server = textBox3.Text;
            try
            {
                port = Convert.ToInt32(textBox4.Text);
            }
            catch
            {
                this.textBox2.AppendText("Please enter the target Port.\r\n");
            }


            if (server == null)
            {
                this.textBox2.AppendText("Please enter the target IP Address.\r\n");
            }
            else
            {
                try
                {
                    _sender.Start(server, port);
                    this.textBox2.AppendText($"Connected to \"{server}\":\"{port}\"\r\n");
                }
                catch
                {
                    this.textBox2.AppendText("Connection failed.\r\n");
                }
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (return_val == null)
            {
                this.textBox2.AppendText("Please create message first.\r\n");
            }
            else
            {
                bool isSended = _sender.SendPackage(return_val);
                if (isSended)
                {
                    this.textBox2.AppendText("Packet was sent. \r\n");
                }
                else
                {
                    this.textBox2.AppendText("Please make a connection first. \r\n");                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _sender.Stop();
            this.textBox2.AppendText("Disconnected. \r\n");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox2.AppendText("Connection Status: " + Convert.ToString(_sender.IsConnected()) + "\r\n");
        }
    }
}
