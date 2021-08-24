using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackGuide.Entities.Guide
{
    public class GuideEnums
    {
        public enum Hardware
        {
            [Description("Vita 1xxx Model (Phat)")]
            Vita_Fat,
            [Description("Vita 2xxx Model (Slim)")]
            Vita_Slim,
            [Description("PS (Vita) TV")]
            Vita_TV
        }

        public enum FatModel
        {
            Not_Sure_Not_Japanese = 1,
            Not_Sure_Japanese,
            PCH1000,
            PCH1001,
            PCH1002,
            PCH1003,
            PCH1004,
            PCH1005,
            PCH1006,
            PCH1007,
            PCH1008,
            PCH1009,
            PCH1010,
            PCH1101,
            PCH1102,
            PCH1103,
            PCH1104,
            PCH1105,
            PCH1106,
            PCH1107,
            PCH1108,
            PCH1109,
            PCH1110
        }

        public enum SlimModel
        {
            Not_Sure_Not_Japanese = 1,
            Not_Sure_Japanese,
            PCH2000,
            PCH2001,
            PCH2002,
            PCH2003,
            PCH2004,
            PCH2005,
            PCH2006,
            PCH2007,
            PCH2008,
            PCH2009,
            PCH2010,
            PCH2016
        }

        public enum TVModel
        {
            Not_Sure_Not_Japanese = 1,
            Not_Sure_Japanese,
            VTE1000,
            VTE1001,
            VTE1002,
            VTE1003,
            VTE1004,
            VTE1005,
            VTE1006,
            VTE1007,
            VTE1008,
            VTE1009,
            VTE1010
        }

        public enum OfficialAcessories
        {
            Official_4GB = 1,
            Official_8GB,
            Official_16GB,
            Official_32GB,
            Official_64GB
        }

        [Flags]
        public enum ModAcessories
        {
            SD2Vita = 1,
            PSVSD = 2,
            USB_Storage = 4
        }

        //Telemetry™ lol
        public enum SDCardSize
        {
            microSD_1GB = 1,
            microSD_2GB,
            microSD_4GB,
            microSD_8GB,
            microSD_16GB,
            microSD_32GB,
            microSD_64GB,
            microSD_128GB,
            microSD_200GB,
            microSD_256GB,
            microSD_400GB,
            microSD_512GB,
            microSD_1TB,
        }

        public enum CurrentFirmware
        {
            Under360,
            On360,
            Over360_Under365,
            On365,
            Over365
        }

        public enum DestinationFirmware
        {
            Henkaku_Enso_360,
            Henkaku_Enso_365,
            Henkaku_368
        }

        public enum SoftwareState
        {
            Vanilla,
            Already_Hacked
        }
    }
}
