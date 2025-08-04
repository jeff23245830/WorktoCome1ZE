using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBox1.SelectedIndex = 0; // 預設選擇第一個 COM port
            }

            button1.Text = "連線";
            label1.Text = "未連線";

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 180; // 設定滑動軸範圍為 0 到 180 度
            trackBar1.Value = 90;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = 9600; // 鮑率要和 Arduino 一致
                    serialPort1.Open();
                    button1.Text = "中斷連線";
                    label1.Text = "已連線";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("連線失敗：" + ex.Message);
                }
            }
            else
            {
                serialPort1.Close();
                button1.Text = "連線";
                label1.Text = "未連線";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // 檢查序列埠是否開啟
            if (serialPort1.IsOpen)
            {
                // 取得滑動軸目前的值
                int angle = trackBar1.Value;

                // 將角度值轉換為字串
                string angleString = angle.ToString();

                try
                {
                    // 將字串傳送給 Arduino
                    serialPort1.WriteLine(angleString);
                    label2.Text = "角度：" + angleString;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("傳送資料失敗：" + ex.Message);
                    serialPort1.Close();
                    button1.Text = "連線";
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close(); // 程式關閉時，確保序列埠也關閉
            }
        }
    }
}
