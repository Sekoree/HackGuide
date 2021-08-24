using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackGuide.Entities.Guide
{
    public class VitaDevice
    {
        public ulong  Id { get; set; }
        public GuideEnums.Hardware Hardware { get; set; }
        public GuideEnums.FatModel FatModel { get; set; } = 0;
        public GuideEnums.SlimModel SlimModel { get; set; } = 0;
        public GuideEnums.TVModel VitaTVModel { get; set; } = 0;
        public GuideEnums.OfficialAcessories MemoryCard { get; set; } = 0;
        public bool IMCUnlocked { get; set; } = false;
        public GuideEnums.ModAcessories ModAcessories { get; set; } = 0;
        public GuideEnums.SDCardSize SDCardSize { get; set; } = 0;
        public GuideEnums.DestinationFirmware TargetFirmware { get; set; }
        public GuideEnums.CurrentFirmware CurrentFirmware { get; set; }
        public GuideEnums.SoftwareState CurrentState { get; set; } = GuideEnums.SoftwareState.Vanilla;
    }
}
