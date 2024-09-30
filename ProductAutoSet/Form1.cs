using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using ProductAutoSet;
using System.IO;
using System.Reflection;

namespace ProductAutoSet
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SQLiteExample SQLCall = new SQLiteExample();
        List<string> productName = new List<string>();
        public DeviceSetting deviceSetting = new DeviceSetting()
        {
            DeviceName = "PX1-HKM-A6QIFRS",
            Step1 = new Step1Setting { Point = "TP8 (1.8V±5%)", Volt_Min = 1.71, Volt_Max = 1.89 },
            Step4 = new Step4Setting { UMEC_SW = "T97" },
            Step5 = new Step5Setting
            {
                Cfg = new ProFile[4]
                        {
                            new ProFile { Verson = "V1.0", FileName = "", Data = new List<string> { "" } },
                            new ProFile { Verson = "V1.0", FileName = "", Data = new List<string> { "" } },
                            new ProFile { Verson = "V1.0", FileName = "", Data = new List<string> { "" } },
                            new ProFile { Verson = "V1.0", FileName = "", Data = new List<string> { "" } }
                        }
            },
            Step6 = new Step6Setting { RFPowerMin0 = 19, RFPowerMax0 = 21, RFPowerMin1 = 1, RFPowerMax1 = 5 },
            Step8 = new Step8Setting { DeviceVer = "F0102" },
            Step9 = new Step9Setting { LED_Volt_Min = 0.5, LED_Volt_Max = 1.71, Reset_Current_Max = 5 }
        };
        public class Global
        {
            public static string OPID, OPNAME, ip, database;
            //public static float offset;
            //public static double errorSpec;

        }
        Form3 f3 = new Form3();
        string owner;
        string dateTime;

        private void Form1_Load(object sender, EventArgs e)
        {
            //f3.ShowDialog();
            //if (Global.OPID == "")
            //{
            //    this.Close();
            //    return;
            //}
            //else
            //{

            //    Owner_LB.Text = Global.OPNAME;
            //}
            Init();
            productName = SQLCall.FilterProductName();
            foreach (string name in productName)
            {
                ProductName_CB.Items.Add(name);
            }
            ProductName_CB.SelectedIndex = 0;
            deviceSetting = SQLCall.ReadSetting(deviceSetting.DeviceName);
        }

        private void WriteBtn_Click(object sender, EventArgs e)
        {

            ControlClose();
            ReadValue();

            bool res = SQLCall.WriteDeviceLog(deviceSetting, dateTime, owner);

            if (res)
            {
                res = SQLCall.WriteCfgLog(deviceSetting, dateTime, owner);
                if (res)
                {
                    res = SQLCall.WriteCfgSetting(deviceSetting, dateTime, owner);
                    if (res)
                    {
                        res = SQLCall.WriteDeviceSetting(deviceSetting, dateTime, owner);
                        if (res)
                        {
                            MessageBox.Show("資料寫入成功");
                        }
                        else
                        {
                            MessageBox.Show("Device資料寫入Setting失敗");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cfg資料寫入Setting失敗");
                    }

                }
                else
                {
                    MessageBox.Show("資料寫入Cfg失敗");
                }

            }
            else
            {
                MessageBox.Show("資料寫入Log失敗");
            }
            ControlOpen();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
        private void Init()
        {
            productName.Clear();
            Owner_LB.Text = "10035";
            ProductName_CB.Text = deviceSetting.DeviceName;
            Point_TB.Text = deviceSetting.Step1.Point;
            Volt_Min_Updown.Value = ((decimal)deviceSetting.Step1.Volt_Min);
            Volt_Max_Updown.Value = ((decimal)deviceSetting.Step1.Volt_Max);
            UMEC_SW_TB.Text = deviceSetting.Step4.UMEC_SW;
            Version1_TB.Text = deviceSetting.Step5.Cfg[0].Verson.ToString();
            Version2_TB.Text = deviceSetting.Step5.Cfg[1].Verson.ToString();
            Version3_TB.Text = deviceSetting.Step5.Cfg[2].Verson.ToString();
            Version4_TB.Text = deviceSetting.Step5.Cfg[3].Verson.ToString();
            RFPowerMin0_Updown.Value = ((decimal)deviceSetting.Step6.RFPowerMin0);
            RFPowerMax0_Updown.Value = ((decimal)deviceSetting.Step6.RFPowerMax0);
            RFPowerMin1_Updown.Value = ((decimal)deviceSetting.Step6.RFPowerMin1);
            RFPowerMax1_Updown.Value = ((decimal)deviceSetting.Step6.RFPowerMax1);
            DeviceVer_TB.Text = deviceSetting.Step8.DeviceVer;
            LED_Volt_Min_Updown.Value = ((decimal)deviceSetting.Step9.LED_Volt_Min);
            LED_Volt_Max_Updown.Value = ((decimal)deviceSetting.Step9.LED_Volt_Max);
            Reset_Current_Max_Updown.Value = deviceSetting.Step9.Reset_Current_Max;

        }
        private void ControlClose()
        {
            ProductName_CB.Enabled = false;
            Point_TB.Enabled = false;
            Volt_Min_Updown.Enabled = false;
            Volt_Max_Updown.Enabled = false;
            UMEC_SW_TB.Enabled = false;
            RFPowerMin0_Updown.Enabled = false;
            RFPowerMax0_Updown.Enabled = false;
            RFPowerMin1_Updown.Enabled = false;
            RFPowerMax1_Updown.Enabled = false;
            DeviceVer_TB.Enabled = false;
            LED_Volt_Min_Updown.Enabled = false;
            LED_Volt_Max_Updown.Enabled = false;
            Reset_Current_Max_Updown.Enabled = false;
            Version1_TB.Enabled = false;
            Version2_TB.Enabled = false;
            Version3_TB.Enabled = false;
            Version4_TB.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

        }
        private void ControlOpen()
        {
            ProductName_CB.Enabled = true;
            Point_TB.Enabled = true;
            Volt_Min_Updown.Enabled = true;
            Volt_Max_Updown.Enabled = true;
            UMEC_SW_TB.Enabled = true;
            RFPowerMin0_Updown.Enabled = true;
            RFPowerMax0_Updown.Enabled = true;
            RFPowerMin1_Updown.Enabled = true;
            RFPowerMax1_Updown.Enabled = true;
            DeviceVer_TB.Enabled = true;
            LED_Volt_Min_Updown.Enabled = true;
            LED_Volt_Max_Updown.Enabled = true;
            Reset_Current_Max_Updown.Enabled = true;
            Version1_TB.Enabled = true;
            Version2_TB.Enabled = true;
            Version3_TB.Enabled = true;
            Version4_TB.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }
        private List<string> ReadStep5Value(int index)
        {
            string richTextContent = "";
            switch (index)
            {
                case 0:
                    richTextContent = Config1_RTB_2.Text;
                    break;
                case 1:
                    richTextContent = Config2_RTB_2.Text;
                    break;
                case 2:
                    richTextContent = Config3_RTB_2.Text;
                    break;
                case 3:
                    richTextContent = Config4_RTB_2.Text;
                    break;
            }
            List<string> lines = richTextContent
                .Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .ToList();
            return lines;
        }
        private void ReadValue()
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            owner = Owner_LB.Text;
            deviceSetting.DeviceName = ProductName_CB.Text;
            deviceSetting.Step1.Point = Point_TB.Text;
            deviceSetting.Step1.Volt_Min = ((double)Volt_Min_Updown.Value);
            deviceSetting.Step1.Volt_Max = ((double)Volt_Max_Updown.Value);
            deviceSetting.Step4.UMEC_SW = UMEC_SW_TB.Text;
            deviceSetting.Step5.Cfg[0].Verson = Version1_TB.Text;
            deviceSetting.Step5.Cfg[1].Verson = Version2_TB.Text;
            deviceSetting.Step5.Cfg[2].Verson = Version3_TB.Text;
            deviceSetting.Step5.Cfg[3].Verson = Version4_TB.Text;
            deviceSetting.Step5.Cfg[0].Data = ReadStep5Value(0);
            deviceSetting.Step5.Cfg[1].Data = ReadStep5Value(1);
            deviceSetting.Step5.Cfg[2].Data = ReadStep5Value(2);
            deviceSetting.Step5.Cfg[3].Data = ReadStep5Value(3);
            deviceSetting.Step6.RFPowerMin0 = ((double)RFPowerMin0_Updown.Value);
            deviceSetting.Step6.RFPowerMax0 = ((double)RFPowerMax0_Updown.Value);
            deviceSetting.Step6.RFPowerMin1 = ((double)RFPowerMin1_Updown.Value);
            deviceSetting.Step6.RFPowerMax1 = ((double)RFPowerMax1_Updown.Value);
            deviceSetting.Step8.DeviceVer = DeviceVer_TB.Text;
            deviceSetting.Step9.LED_Volt_Min = ((double)LED_Volt_Max_Updown.Value);
            deviceSetting.Step9.LED_Volt_Max = ((double)LED_Volt_Max_Updown.Value);
            deviceSetting.Step9.Reset_Current_Max = ((uint)Reset_Current_Max_Updown.Value);
        }
        private bool ReadCfgFile(string filename, int index)
        {
            bool res = false;
            if (String.IsNullOrEmpty(filename))
            {
                deviceSetting.Step5.Cfg[index].Data = new List<string>();
                return true;
            }
            if (!File.Exists(filename)) { return res; }

            string cfg = "";
            string buf;

            StreamReader file = new StreamReader(filename);
            while (file.Peek() > -1)
            {
                buf = file.ReadLine();
                if (!buf.Contains('%'))
                {
                    int commentIndex = buf.IndexOf("//");
                    if (commentIndex != -1)
                    {
                        // 如果有注释，只取注释前的部分
                        buf = buf.Substring(0, commentIndex);
                    }
                    cfg += buf.Trim() + "\n"; // 使用Trim()移除行尾的空白字符
                }
                else
                {
                    cfg += "\n\r";
                }
            }
            var s = cfg.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            deviceSetting.Step5.Cfg[index].Data = s.ToList();

            string searchTerm = "SaveCurrentCfg";
            var filteredLines = s.Where(line => line.Contains(searchTerm)).ToList();
            string ver = filteredLines.LastOrDefault();
            if (string.IsNullOrEmpty(ver))
            {
                deviceSetting.Step5.Cfg[index].Verson = "";
            }
            else
            {
                string[] parts = ver.Split(' ');
                deviceSetting.Step5.Cfg[index].Verson = parts.Last();
            }
            res = true;
            return res;
        }
        private bool ReadCfgFile_original(string filename, int index)
        {
            bool res = false;


            if (string.IsNullOrEmpty(filename))
            {
                return false;
            }


            if (!File.Exists(filename))
            {
                return res;
            }

            string buf;
            string cfg = "";


            using (StreamReader file = new StreamReader(filename))
            {

                RichTextBox selectedRichTextBox = null;
                switch (index)
                {
                    case 0:
                        selectedRichTextBox = Config1_RTB;
                        break;
                    case 1:
                        selectedRichTextBox = Config2_RTB;
                        break;
                    case 2:
                        selectedRichTextBox = Config3_RTB;
                        break;
                    case 3:
                        selectedRichTextBox = Config4_RTB;
                        break;
                }

                if (selectedRichTextBox == null)
                {
                    return res;
                }


                selectedRichTextBox.Clear();


                while (file.Peek() > -1)
                {
                    buf = file.ReadLine();


                    if (buf.Contains("%"))
                    {
                        selectedRichTextBox.SelectionColor = Color.Red;
                    }

                    else if (buf.Contains("//"))
                    {
                        selectedRichTextBox.SelectionColor = Color.Blue;
                    }

                    else
                    {
                        selectedRichTextBox.SelectionColor = selectedRichTextBox.ForeColor;
                    }

                    cfg = buf;
                    selectedRichTextBox.AppendText(cfg + "\n");
                }
            }


            res = true;
            return res;
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            bool res = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Button Button = sender as Button;
                switch (Button.Name)
                {
                    case "button1":
                        Config1_RTB.Clear();
                        Config1_RTB_2.Clear();
                        deviceSetting.Step5.Cfg[0].FileName = openFileDialog.FileName;
                        textBox1.Text = deviceSetting.Step5.Cfg[0].FileName;
                        ReadCfgFile_original(deviceSetting.Step5.Cfg[0].FileName, 0);
                        res = ReadCfgFile(deviceSetting.Step5.Cfg[0].FileName, 0);
                        Version1_TB.Text = deviceSetting.Step5.Cfg[0].Verson;

                        if (res)
                        {
                            foreach (string s in deviceSetting.Step5.Cfg[0].Data)
                            {
                                Config1_RTB_2.Text += s + "\n";
                            }
                        }
                        tabControl1.SelectedIndex = 0;
                        break;
                    case "button2":
                        Config2_RTB.Clear();
                        Config2_RTB_2.Clear();
                        deviceSetting.Step5.Cfg[1].FileName = openFileDialog.FileName;
                        textBox2.Text = deviceSetting.Step5.Cfg[1].FileName;
                        ReadCfgFile_original(deviceSetting.Step5.Cfg[1].FileName, 1);
                        res = ReadCfgFile(deviceSetting.Step5.Cfg[1].FileName, 1);
                        Version2_TB.Text = deviceSetting.Step5.Cfg[1].Verson;

                        if (res)
                        {
                            foreach (string s in deviceSetting.Step5.Cfg[1].Data)
                            {
                                Config2_RTB_2.Text += s + "\n";
                            }
                        }
                        tabControl1.SelectedIndex = 1;
                        break;
                    case "button3":
                        Config3_RTB.Clear();
                        Config3_RTB_2.Clear();
                        deviceSetting.Step5.Cfg[2].FileName = openFileDialog.FileName;
                        textBox3.Text = deviceSetting.Step5.Cfg[2].FileName;
                        ReadCfgFile_original(deviceSetting.Step5.Cfg[2].FileName, 2);
                        res = ReadCfgFile(deviceSetting.Step5.Cfg[2].FileName, 2);
                        Version3_TB.Text = deviceSetting.Step5.Cfg[2].Verson;

                        if (res)
                        {
                            foreach (string s in deviceSetting.Step5.Cfg[2].Data)
                            {
                                Config3_RTB_2.Text += s + "\n";
                            }
                        }
                        tabControl1.SelectedIndex = 2;
                        break;
                    case "button4":
                        Config4_RTB.Clear();
                        Config4_RTB_2.Clear();
                        deviceSetting.Step5.Cfg[3].FileName = openFileDialog.FileName;
                        textBox4.Text = deviceSetting.Step5.Cfg[3].FileName;
                        ReadCfgFile_original(deviceSetting.Step5.Cfg[3].FileName, 3);
                        res = ReadCfgFile(deviceSetting.Step5.Cfg[3].FileName, 3);
                        Version4_TB.Text = deviceSetting.Step5.Cfg[3].Verson;

                        if (res)
                        {
                            foreach (string s in deviceSetting.Step5.Cfg[3].Data)
                            {
                                Config4_RTB_2.Text += s + "\n";
                            }
                        }
                        tabControl1.SelectedIndex = 3;
                        break;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = tabControl1.SelectedIndex;
        }

        private void tabControl2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = tabControl2.SelectedIndex;
        }
        private int GetLineNoVscroll(RichTextBox rtb)
        {
            //获得当前坐标信息
            Point p = rtb.Location;
            int crntFirstIndex = rtb.GetCharIndexFromPosition(p);
            int crntFirstLine = rtb.GetLineFromCharIndex(crntFirstIndex);
            return crntFirstLine;
        }
        private void TrunRowsId(int iCodeRowsID, RichTextBox rtb)
        {
            try
            {
                rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(iCodeRowsID);
                rtb.SelectionLength = 0;
                rtb.ScrollToCaret();
            }
            catch
            {

            }
        }

        private void Config4_RTB_VScroll(object sender, EventArgs e)
        {
            int wucha = 0;
            int crntLastLine = GetLineNoVscroll(Config4_RTB) - wucha;
            TrunRowsId(crntLastLine, Config4_RTB_2);
        }

        private void Config3_RTB_VScroll(object sender, EventArgs e)
        {
            int wucha = 0;
            int crntLastLine = GetLineNoVscroll(Config3_RTB) - wucha;
            TrunRowsId(crntLastLine, Config3_RTB_2);
        }

        private void Config2_RTB_VScroll(object sender, EventArgs e)
        {
            int wucha = 0;
            int crntLastLine = GetLineNoVscroll(Config2_RTB) - wucha;
            TrunRowsId(crntLastLine, Config2_RTB_2);
        }

        private void Config1_RTB_VScroll(object sender, EventArgs e)
        {
            int wucha = 0;
            int crntLastLine = GetLineNoVscroll(Config1_RTB) - wucha;
            TrunRowsId(crntLastLine, Config1_RTB_2);
        }
    }
}
