using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductAutoSet
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SQLiteExample SQLCall = new SQLiteExample();
        List<string> productName = new List<string>();
        public enum Source
        {
            Setting,
            Log
        }
        Source source = Source.Setting;

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            
            int cnt = 0;
            DataTable dt = new DataTable();
            dt.Clear();
                     
            if (ProductName_CB.Text != "")
            {
                cnt++;
            }
            
            if (cnt == 0)
            {
                dt = SQLCall.Search(source);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            else
            {
                string productname = ProductName_CB.Text;
                if (ProductName_CB.Text !="")
                {
                    
                    dt =SQLCall.Search(source,productname);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
            }
        }


        private void OutputBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString();
                                if (i != columnCount - 1)
                                    columnNames += ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString();
                                    if (j != columnCount - 1)
                                        outputCsv[i] += ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("資料匯出完成!", "匯出");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("無匯出資料!", "匯出");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            if(selectedItem == "更改紀錄")
            {
                source = Source.Log;
            }
            else
            {
                source = Source.Setting;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            productName = SQLCall.FilterProductName();
            foreach (string name in productName)
            {
                ProductName_CB.Items.Add(name);
            }
            
            comboBox1.SelectedIndex = 0;
        }
    }
}
