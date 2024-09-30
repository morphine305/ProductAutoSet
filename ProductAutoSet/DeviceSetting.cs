using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAutoSet
{
    #region DeviceSetting
        public class DeviceSetting
        {
            public string DeviceName { get; set; }
            public Step1Setting Step1 { get; set; }
            public Step4Setting Step4 { get; set; }
            public Step5Setting Step5 { get; set; }
            public Step6Setting Step6 { get; set; }
            public Step8Setting Step8 { get; set; }
            public Step9Setting Step9 { get; set; }

           
        }

        public class Step1Setting
        {
            public string Point { get; set; } = "";
            public double Volt_Min { get; set; } = 0;
            public double Volt_Max { get; set; } = 0;
        }
        public class Step4Setting
        {
            public string UMEC_SW { get; set; } = "T98";
        }

        public class Step5Setting
        {
            public ProFile[] Cfg { get; set; } = new ProFile[4];
        }

        public class Step6Setting
        {
            public double RFPowerMin0 { get; set; } = 0;
            public double RFPowerMax0 { get; set; } = 0;
            public double RFPowerMin1 { get; set; } = 0;
            public double RFPowerMax1 { get; set; } = 0;
        }

        public class Step8Setting
        {
            public string DeviceVer { get; set; } = "";
        }

        public class Step9Setting
        {
            public double LED_Volt_Min { get; set; } = 0;
            public double LED_Volt_Max { get; set; } = 0;
            public uint Reset_Current_Max { get; set; } = 0;
        }
        public class ProFile
        {
            public string Verson { get; set; } = "";
            public string FileName { get; set; } = "";
            public List<string> Data { get; set; } = new List<string>();
        }
        
        #endregion
}
