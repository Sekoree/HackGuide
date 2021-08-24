using HackGuide.PsnUtil.Entities;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HackGuide.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var arg1 = $"-m";
            var arg2 = $"\"{Directory.GetCurrentDirectory()}\\VITASHELL\\app.psvmd-dec\"";
            var arg3 = $"-K";
            var arg4 = $"373e02f6fe7f46ecffe65d898d1d86e6a4d144510301b99091f216eb717ec0f0";
            var arg5 = $"\"{Directory.GetCurrentDirectory()}\\VITASHELL\"";
            var arg6 = $"\"{Directory.GetCurrentDirectory()}\\Backup\"";
        }
    }
}