using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductAutoSet;

namespace ProductAutoSet
{
    public class SQLiteExample
    {
        private string dbFilePath = "D:\\James\\sideproject\\ProductAutoSet\\DataBase.db";
        private string connectionString;
        

        public SQLiteExample()
        {
            connectionString = $"Data Source={dbFilePath};Version=3;";
        }
        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        public List<string> FilterProductName()
        {
            List<string> ProductName = new List<string>();
            using (var connection = GetConnection())
            {
                connection.Open();

                string query =
                "SELECT distinct\r\n    data.DeviceName\r\nFROM \r\n    DATA";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string Product = reader.GetString(0);
                            ProductName.Add(Product);
                        }
                    }
                }

            }
            return ProductName;
        }
        public bool WriteDeviceLog(DeviceSetting deviceSetting, string dateTime, string Owner)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO ProgramLog    " +
                    "(DeviceName, Point, Volt_Min, Volt_Max, UMEC_SW, RFPowerMin0, RFPowerMax0, RFPowerMin1, RFPowerMax1, DeviceVer, LED_Volt_Min, LED_Volt_Max, Reset_Current_Max,DateTime,Owner) " +
                    "VALUES (@DeviceName, @Point, @Volt_Min, @Volt_Max, @UMEC_SW, @RFPowerMin0, @RFPowerMax0, @RFPowerMin1, @RFPowerMax1, @DeviceVer, @LED_Volt_Min, @LED_Volt_Max, @Reset_Current_Max,@DateTime,@Owner)";
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceName", deviceSetting.DeviceName);
                        command.Parameters.AddWithValue("@Point", deviceSetting.Step1.Point);
                        command.Parameters.AddWithValue("@Volt_Min", deviceSetting.Step1.Volt_Min);
                        command.Parameters.AddWithValue("@Volt_Max", deviceSetting.Step1.Volt_Max);
                        command.Parameters.AddWithValue("@UMEC_SW", deviceSetting.Step4.UMEC_SW);
                        command.Parameters.AddWithValue("@RFPowerMin0", deviceSetting.Step6.RFPowerMin0);
                        command.Parameters.AddWithValue("@RFPowerMax0", deviceSetting.Step6.RFPowerMax0);
                        command.Parameters.AddWithValue("@RFPowerMin1", deviceSetting.Step6.RFPowerMin1);
                        command.Parameters.AddWithValue("@RFPowerMax1", deviceSetting.Step6.RFPowerMax1);
                        command.Parameters.AddWithValue("@DeviceVer", deviceSetting.Step8.DeviceVer);
                        command.Parameters.AddWithValue("@LED_Volt_Min", deviceSetting.Step9.LED_Volt_Min);
                        command.Parameters.AddWithValue("@LED_Volt_Max", deviceSetting.Step9.LED_Volt_Max);
                        command.Parameters.AddWithValue("@Reset_Current_Max", deviceSetting.Step9.Reset_Current_Max);
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Owner", Owner);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show($"SQLite error: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General error: {ex.Message}");
                    return false;
                }

            }
            return true;
        }
        public bool WriteDeviceSetting(DeviceSetting deviceSetting, string dateTime, string Owner)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT OR REPLACE INTO DeviceSetting " +
                    "(DeviceName, Point, Volt_Min, Volt_Max, UMEC_SW, RFPowerMin0, RFPowerMax0, RFPowerMin1, RFPowerMax1, DeviceVer, LED_Volt_Min, LED_Volt_Max, Reset_Current_Max,DateTime,Owner) " +
                    "VALUES (@DeviceName, @Point, @Volt_Min, @Volt_Max, @UMEC_SW, @RFPowerMin0, @RFPowerMax0, @RFPowerMin1, @RFPowerMax1, @DeviceVer, @LED_Volt_Min, @LED_Volt_Max, @Reset_Current_Max,@DateTime,@Owner)";
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceName", deviceSetting.DeviceName);
                        command.Parameters.AddWithValue("@Point", deviceSetting.Step1.Point);
                        command.Parameters.AddWithValue("@Volt_Min", deviceSetting.Step1.Volt_Min);
                        command.Parameters.AddWithValue("@Volt_Max", deviceSetting.Step1.Volt_Max);
                        command.Parameters.AddWithValue("@UMEC_SW", deviceSetting.Step4.UMEC_SW);
                        command.Parameters.AddWithValue("@RFPowerMin0", deviceSetting.Step6.RFPowerMin0);
                        command.Parameters.AddWithValue("@RFPowerMax0", deviceSetting.Step6.RFPowerMax0);
                        command.Parameters.AddWithValue("@RFPowerMin1", deviceSetting.Step6.RFPowerMin1);
                        command.Parameters.AddWithValue("@RFPowerMax1", deviceSetting.Step6.RFPowerMax1);
                        command.Parameters.AddWithValue("@DeviceVer", deviceSetting.Step8.DeviceVer);
                        command.Parameters.AddWithValue("@LED_Volt_Min", deviceSetting.Step9.LED_Volt_Min);
                        command.Parameters.AddWithValue("@LED_Volt_Max", deviceSetting.Step9.LED_Volt_Max);
                        command.Parameters.AddWithValue("@Reset_Current_Max", deviceSetting.Step9.Reset_Current_Max);
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Owner", Owner);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show($"SQLite error: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General error: {ex.Message}");
                    return false;
                }

            }
            return true;
        }
        public bool WriteCfgLog(DeviceSetting deviceSetting, string dateTime, string Owner)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                #region 一個command 一個欄位
                //for(int i =0; i < deviceSetting.Step5.Cfg.Length; i++)
                //{
                //    for(int j = 0; j < deviceSetting.Step5.Cfg[i].Data.Count; j++)      // j => 每一個Cfg檔內的每一句command 順序
                //    {
                //        string s = deviceSetting.Step5.Cfg[i].Data[j];
                //        string query = "INSERT OR REPLACE INTO ConfigureLog(DeviceName,ID,DATA,Version,DateTime,Owner,SerialNum) VALUES (@DeviceName,@ID,@Data,@Version,@DateTime,@Owner,@SerialNum)";
                //        try
                //        {
                //            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                //            {
                //                command.Parameters.AddWithValue("@DeviceName", deviceSetting.DeviceName);
                //                command.Parameters.AddWithValue("@ID", i);
                //                command.Parameters.AddWithValue("@Data", s);
                //                command.Parameters.AddWithValue("@Version", deviceSetting.Step5.Cfg[i].Verson);
                //                command.Parameters.AddWithValue("@DateTime", dateTime);
                //                command.Parameters.AddWithValue("@Owner", Owner);
                //                command.Parameters.AddWithValue("@SerialNum",j);

                //                command.ExecuteNonQuery();
                //            }
                //        }
                //        catch (SQLiteException ex)
                //        {
                //            MessageBox.Show($"SQLite error: {ex.Message}");
                //            return false;
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show($"General error: {ex.Message}");
                //            return false;
                //        }
                //    }
                //}
                #endregion
                #region 一個Cfg 一個欄位
                for (int i = 0; i < deviceSetting.Step5.Cfg.Length; i++)
                {
                    string x = "";
                    foreach (string s in deviceSetting.Step5.Cfg[i].Data)
                    {
                        x += s + "\n";
                    }
                    if (x == "")
                    {
                        x = "";
                    }

                    string table = $"ConfigureLog{i}";
                    string query = $"INSERT INTO {table} (DeviceName,DATA,Version,DateTime,Owner) VALUES (@DeviceName,@Data,@Version,@DateTime,@Owner)";
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@DeviceName", deviceSetting.DeviceName);
                            command.Parameters.AddWithValue("@Data", x);
                            command.Parameters.AddWithValue("@Version", deviceSetting.Step5.Cfg[i].Verson);
                            command.Parameters.AddWithValue("@DateTime", dateTime);
                            command.Parameters.AddWithValue("@Owner", Owner);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show($"SQLite error: {ex.Message}");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"General error: {ex.Message}");
                        return false;
                    }



                }
                #endregion
            }
            return true;
        }
        public bool WriteCfgSetting(DeviceSetting deviceSetting, string dateTime, string Owner)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                for (int i = 0; i < deviceSetting.Step5.Cfg.Length; i++)
                {
                    string query = $"INSERT OR REPLACE INTO ConfigureSetting{i} (DeviceName, Data, Version, DateTime, Owner) SELECT DeviceName, Data, Version, DateTime, Owner FROM ( SELECT DeviceName, Data, Version, DateTime, Owner FROM ConfigureLog{i} WHERE DateTime = ( SELECT MAX(DateTime) FROM ConfigureLog{i} AS sub WHERE sub.DeviceName = ConfigureLog{i}.DeviceName));";
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show($"SQLite error: {ex.Message}");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"General error: {ex.Message}");
                        return false;
                    }
                }

            }
            return true;
        }
        public DeviceSetting ReadSetting(string productName)
        {
            DeviceSetting deviceSetting = new DeviceSetting()
            {
                Step1 = new Step1Setting(),
                Step4 = new Step4Setting(),
                Step5 = new Step5Setting()
                {
                    Cfg = new ProFile[4]
                    {
                        new ProFile { Verson = "", FileName = "", Data = new List<string> { "" } },
                        new ProFile { Verson = "", FileName = "", Data = new List<string> { "" } },
                        new ProFile { Verson = "", FileName = "", Data = new List<string> { "" } },
                        new ProFile { Verson = "", FileName = "", Data = new List<string> { "" } }
                    }
                },
                Step6 = new Step6Setting(),
                Step8 = new Step8Setting(),
                Step9 = new Step9Setting()
            };
            List<string> CfgData0 = new List<string>();
            List<string> CfgData1 = new List<string>();
            List<string> CfgData2 = new List<string>();
            List<string> CfgData3 = new List<string>();
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT \r\n    DeviceSetting.DeviceName,\r\n    DeviceSetting.Point,\r\n    DeviceSetting.Volt_Min,\r\n    DeviceSetting.Volt_Max,\r\n    DeviceSetting.UMEC_SW,\r\n    DeviceSetting.RFPowerMin0,\r\n    DeviceSetting.RFPowerMax0,\r\n    DeviceSetting.RFPowerMin1,\r\n    DeviceSetting.RFPowerMax1,\r\n    DeviceSetting.DeviceVer,\r\n    DeviceSetting.LED_Volt_Min,\r\n    DeviceSetting.LED_Volt_Max,\r\n    DeviceSetting.Reset_Current_Max,\r\n    ConfigureSetting0.Data AS DATA0,\r\n    ConfigureSetting0.Version AS Cfg0_Version,\r\n    ConfigureSetting1.Data AS DATA1,\r\n    ConfigureSetting1.Version AS Cfg1_Version,\r\n    ConfigureSetting2.Data AS DATA2,\r\n    ConfigureSetting2.Version AS Cfg2_Version,\r\n    ConfigureSetting3.Data AS DATA3,\r\n    ConfigureSetting3.Version AS Cfg3_Version,\r\n    ConfigureSetting0.\"DateTime\",\r\n    DeviceSetting.\"Owner\"\r\nFROM \r\n    ProgramSet\r\nLEFT JOIN \r\n    DeviceSetting ON ProgramSet.DeviceName = DeviceSetting.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting0 ON ProgramSet.DeviceName = ConfigureSetting0.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting1 ON ProgramSet.DeviceName = ConfigureSetting1.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting2 ON ProgramSet.DeviceName = ConfigureSetting2.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting3 ON ProgramSet.DeviceName = ConfigureSetting3.DeviceName  WHERE ProgramSet.DeviceName =@DeviceName";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DeviceName", productName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            deviceSetting.DeviceName = reader["DeviceName"].ToString();
                            deviceSetting.Step1.Point = reader["Point"].ToString();
                            deviceSetting.Step1.Volt_Min = Convert.ToDouble(reader["Volt_Min"]);
                            deviceSetting.Step1.Volt_Max = Convert.ToDouble(reader["Volt_Max"]);
                            deviceSetting.Step4.UMEC_SW = reader["UMEC_SW"].ToString();
                            string cfg0 = reader["DATA0"].ToString();
                            CfgData0 = cfg0.Split('\n').ToList();
                            deviceSetting.Step5.Cfg[0].Data = CfgData0;
                            string cfg1 = reader["DATA1"].ToString();
                            CfgData1 = cfg1.Split('\n').ToList();
                            deviceSetting.Step5.Cfg[1].Data = CfgData1;
                            string cfg2 = reader["DATA2"].ToString();
                            CfgData0 = cfg2.Split('\n').ToList();
                            deviceSetting.Step5.Cfg[2].Data = CfgData2;
                            string cfg3 = reader["DATA3"].ToString();
                            CfgData0 = cfg3.Split('\n').ToList();
                            deviceSetting.Step5.Cfg[3].Data = CfgData3;
                            deviceSetting.Step5.Cfg[0].Verson = reader["Cfg0_Version"].ToString();
                            deviceSetting.Step5.Cfg[1].Verson = reader["Cfg1_Version"].ToString();
                            deviceSetting.Step5.Cfg[2].Verson = reader["Cfg2_Version"].ToString();
                            deviceSetting.Step5.Cfg[3].Verson = reader["Cfg3_Version"].ToString();
                            deviceSetting.Step6.RFPowerMin0 = Convert.ToDouble(reader["RFPowerMin0"]);
                            deviceSetting.Step6.RFPowerMax0 = Convert.ToDouble(reader["RFPowerMax0"]);
                            deviceSetting.Step6.RFPowerMin1 = Convert.ToDouble(reader["RFPowerMin1"]);
                            deviceSetting.Step6.RFPowerMax1 = Convert.ToDouble(reader["RFPowerMax1"]);
                            deviceSetting.Step8.DeviceVer = reader["DeviceVer"].ToString();
                            deviceSetting.Step9.LED_Volt_Min = Convert.ToDouble(reader["LED_Volt_Min"]);
                            deviceSetting.Step9.LED_Volt_Max = Convert.ToDouble(reader["LED_Volt_Max"]);
                            deviceSetting.Step9.Reset_Current_Max = Convert.ToUInt32(reader["Reset_Current_Max"]);
                        }
                    }
                }
            }
            return deviceSetting;
        }
        public DataTable Search(Form2.Source source)
        {
            DataTable dt = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                if (source == Form2.Source.Log)
                {
                    string query = "SELECT\r\n\t ProgramLog.DeviceName,\r\n\t ProgramLog.Point,\r\n    ProgramLog.Volt_Min,\r\n    ProgramLog.Volt_Max,\r\n    ProgramLog.UMEC_SW,\r\n    ProgramLog.RFPowerMin0,\r\n    ProgramLog.RFPowerMax0,\r\n    ProgramLog.RFPowerMin1,\r\n    ProgramLog.RFPowerMax1,\r\n    ProgramLog.DeviceVer,\r\n    ProgramLog.LED_Volt_Min,\r\n    ProgramLog.LED_Volt_Max,\r\n    ProgramLog.Reset_Current_Max,\r\n    ConfigureLog0.Data,\r\n    ConfigureLog0.Version AS Cfg0_Version,\r\n    ConfigureLog1.Data,\r\n    ConfigureLog1.Version AS Cfg1_Version,\r\n    ConfigureLog2.Data,\r\n    ConfigureLog2.Version AS Cfg2_Version,\r\n    ConfigureLog3.Data,\r\n    ConfigureLog3.Version AS Cfg3_Version,\r\n    ConfigureLog0.\"DateTime\",\r\n    ProgramLog.\"Owner\"\r\nFROM \r\n\t ProgramLog \r\nJOIN \r\n\t ConfigureLog0\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog0.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog0.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog0.Owner\r\nJOIN \r\n\t ConfigureLog1\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog1.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog1.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog1.Owner\r\nJOIN \r\n\t ConfigureLog2\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog2.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog2.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog2.Owner\r\nJOIN \r\n\t ConfigureLog3\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog3.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog3.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog3.Owner";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                else
                {
                    string query = "SELECT\r\n\t DeviceSetting.DeviceName, \r\n    DeviceSetting.Point,\r\n    DeviceSetting.Volt_Min,\r\n    DeviceSetting.Volt_Max,\r\n    DeviceSetting.UMEC_SW,\r\n    DeviceSetting.RFPowerMin0,\r\n    DeviceSetting.RFPowerMax0,\r\n    DeviceSetting.RFPowerMin1,\r\n    DeviceSetting.RFPowerMax1,\r\n    DeviceSetting.DeviceVer,\r\n    DeviceSetting.LED_Volt_Min,\r\n    DeviceSetting.LED_Volt_Max,\r\n    DeviceSetting.Reset_Current_Max,\r\n    ConfigureSetting0.Data,\r\n    ConfigureSetting0.Version AS Cfg0_Version,\r\n    ConfigureSetting1.Data,\r\n    ConfigureSetting1.Version AS Cfg1_Version,\r\n    ConfigureSetting2.Data,\r\n    ConfigureSetting2.Version AS Cfg2_Version,\r\n    ConfigureSetting3.Data,\r\n    ConfigureSetting3.Version AS Cfg3_Version,\r\n    ConfigureSetting0.DateTime AS Cfg_DateTime,\r\n    DeviceSetting.Owner\r\n    \r\nFROM \r\n    DeviceSetting\r\nLEFT JOIN \r\n    ConfigureSetting0 ON DeviceSetting.DeviceName = ConfigureSetting0.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting1 ON DeviceSetting.DeviceName = ConfigureSetting1.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting2 ON DeviceSetting.DeviceName = ConfigureSetting2.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting3 ON DeviceSetting.DeviceName = ConfigureSetting3.DeviceName\r\nORDER BY\r\n    DeviceSetting.DeviceName ASC;";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }

            }
            return dt;
        }
        public DataTable Search(Form2.Source source, string productName)
        {
            DataTable dt = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                if (source == Form2.Source.Log)
                {
                    string query = "SELECT\r\n\t ProgramLog.DeviceName,\r\n\t ProgramLog.Point,\r\n    ProgramLog.Volt_Min,\r\n    ProgramLog.Volt_Max,\r\n    ProgramLog.UMEC_SW,\r\n    ProgramLog.RFPowerMin0,\r\n    ProgramLog.RFPowerMax0,\r\n    ProgramLog.RFPowerMin1,\r\n    ProgramLog.RFPowerMax1,\r\n    ProgramLog.DeviceVer,\r\n    ProgramLog.LED_Volt_Min,\r\n    ProgramLog.LED_Volt_Max,\r\n    ProgramLog.Reset_Current_Max,\r\n    ConfigureLog0.Data,\r\n    ConfigureLog0.Version AS Cfg0_Version,\r\n    ConfigureLog1.Data,\r\n    ConfigureLog1.Version AS Cfg1_Version,\r\n    ConfigureLog2.Data,\r\n    ConfigureLog2.Version AS Cfg2_Version,\r\n    ConfigureLog3.Data,\r\n    ConfigureLog3.Version AS Cfg3_Version,\r\n    ConfigureLog0.\"DateTime\",\r\n    ProgramLog.\"Owner\"\r\nFROM \r\n\t ProgramLog \r\nJOIN \r\n\t ConfigureLog0\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog0.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog0.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog0.Owner\r\nJOIN \r\n\t ConfigureLog1\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog1.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog1.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog1.Owner\r\nJOIN \r\n\t ConfigureLog2\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog2.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog2.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog2.Owner\r\nJOIN \r\n\t ConfigureLog3\r\n\t ON\r\n    ProgramLog.DeviceName = ConfigureLog3.DeviceName\r\n    AND ProgramLog.DateTime = ConfigureLog3.DateTime\r\n    AND ProgramLog.Owner = ConfigureLog3.Owner WHERE ProgramLog.DeviceName = @DeviceName";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceName", productName);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                else
                {
                    string query = "SELECT \r\n    DeviceSetting.DeviceName,\r\n    DeviceSetting.Point,\r\n    DeviceSetting.Volt_Min,\r\n    DeviceSetting.Volt_Max,\r\n    DeviceSetting.UMEC_SW,\r\n    DeviceSetting.RFPowerMin0,\r\n    DeviceSetting.RFPowerMax0,\r\n    DeviceSetting.RFPowerMin1,\r\n    DeviceSetting.RFPowerMax1,\r\n    DeviceSetting.DeviceVer,\r\n    DeviceSetting.LED_Volt_Min,\r\n    DeviceSetting.LED_Volt_Max,\r\n    DeviceSetting.Reset_Current_Max,\r\n    ConfigureSetting0.Data,\r\n    ConfigureSetting0.Version AS Cfg0_Version,\r\n    ConfigureSetting1.Data,\r\n    ConfigureSetting1.Version AS Cfg1_Version,\r\n    ConfigureSetting2.Data,\r\n    ConfigureSetting2.Version AS Cfg2_Version,\r\n    ConfigureSetting3.Data,\r\n    ConfigureSetting3.Version AS Cfg3_Version,\r\n    ConfigureSetting0.\"DateTime\",\r\n    DeviceSetting.\"Owner\"\r\n    \r\nFROM \r\n    ProgramSet\r\nLEFT JOIN \r\n    DeviceSetting ON ProgramSet.DeviceName = DeviceSetting.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting0 ON ProgramSet.DeviceName = ConfigureSetting0.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting1 ON ProgramSet.DeviceName = ConfigureSetting1.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting2 ON ProgramSet.DeviceName = ConfigureSetting2.DeviceName\r\nLEFT JOIN \r\n    ConfigureSetting3 ON ProgramSet.DeviceName = ConfigureSetting3.DeviceName WHERE ProgramSet.DeviceName =@DeviceName";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceName", productName);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }

            }
            return dt;
        }
        
        
        public bool Match(string useraccount)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM User WHERE UserAccount = @UserAccount";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserAccount", useraccount);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool Match(string useraccount, string userpassword)
        {

            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM User WHERE UserAccount = @UserAccount AND Userpassword = @Userpassword";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserAccount", useraccount);
                    command.Parameters.AddWithValue("@Userpassword", userpassword);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
