using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HBController : ControllerBase
    {
        private readonly ILogger<HBController> _logger;
        private readonly HttpClient _httpClient;

        public HBController(ILogger<HBController> logger,
            HttpClient httpClient)
        {
            Console.WriteLine("Hello from Controler");
            this._logger = logger;
            this._httpClient = httpClient;
        }

        [HttpGet]
        [Route("getfull/{aid}")]
        public async Task<ActionResult> GetFullHBs(string aid)
        {
            var tokenSite = await _httpClient.GetStringAsync("http://cma.henkaku.xyz/?aid=" + aid);

            if (tokenSite.Contains("invalid")) 
                return this.BadRequest();

            var ind = tokenSite.LastIndexOf(' ') + 1;
            var token = tokenSite.Substring(ind, tokenSite.Length - ind);
            token = token.Replace("\n", "");

            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/temp/{aid}"))
            {
                var d = await CreateHB(Homebrews.PCSG90096, aid, token);
                var d2 = await CreateHB(Homebrews.SKGD3PL0Y, aid, token);
                var d3 = await CreateHB(Homebrews.VITASHELL, aid, token);
            }

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                CreateEntryFromAny(archive, $"{Directory.GetCurrentDirectory()}/temp/{aid}/PCSG90096");
                CreateEntryFromAny(archive, $"{Directory.GetCurrentDirectory()}/temp/{aid}/VITASHELL");
                CreateEntryFromAny(archive, $"{Directory.GetCurrentDirectory()}/temp/{aid}/SKGD3PL0Y");
            }
            memoryStream.Position = 0;

            return this.File(memoryStream, "application/zip");
        }

        [HttpGet]
        [Route("geth2/{aid}")]
        public async Task<ActionResult> GetHencore2(string aid)
        {
            var tokenSite = await _httpClient.GetStringAsync("http://cma.henkaku.xyz/?aid=" + aid);

            if (tokenSite.Contains("invalid"))
                return this.BadRequest();

            var ind = tokenSite.LastIndexOf(' ') + 1;
            var token = tokenSite.Substring(ind, tokenSite.Length - ind);
            token = token.Replace("\n", "");

            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/temp/{aid}"))
            {
                var d = await CreateHB(Homebrews.PCSG90096, aid, token);
                var d2 = await CreateHB(Homebrews.SKGD3PL0Y, aid, token);
                var d3 = await CreateHB(Homebrews.VITASHELL, aid, token);
            }

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                CreateEntryFromAny(archive, $"{Directory.GetCurrentDirectory()}/temp/{aid}/PCSG90096");
            }
            memoryStream.Position = 0;

            return this.File(memoryStream, "application/zip");
        }

        [HttpGet]
        [Route("getvs/{aid}")]
        public async Task<ActionResult> GetVitashell(string aid)
        {
            var tokenSite = await _httpClient.GetStringAsync("http://cma.henkaku.xyz/?aid=" + aid);

            if (tokenSite.Contains("invalid"))
                return this.BadRequest();

            var ind = tokenSite.LastIndexOf(' ') + 1;
            var token = tokenSite.Substring(ind, tokenSite.Length - ind);
            token = token.Replace("\n", "");

            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/temp/{aid}"))
            {
                var d = await CreateHB(Homebrews.PCSG90096, aid, token);
                var d2 = await CreateHB(Homebrews.SKGD3PL0Y, aid, token);
                var d3 = await CreateHB(Homebrews.VITASHELL, aid, token);
            }

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                CreateEntryFromAny(archive, $"{Directory.GetCurrentDirectory()}/temp/{aid}/VITASHELL");
            }
            memoryStream.Position = 0;

            return this.File(memoryStream, "application/zip");
        }

        [Flags]
        enum Homebrews
        {
            PCSG90096,
            SKGD3PL0Y,
            VITASHELL
        }

        private async Task<bool> CreateHB(Homebrews homebrew, string aid, string key)
        {
            var curtentDir = Directory.GetCurrentDirectory();
            string[] operations = new[] { "savedata", "license", "appmeta", "app" };
            Process psvimg = default;

            foreach (var item in operations)
            {
                Directory.CreateDirectory($"{curtentDir}/temp/{aid}/{homebrew}/{item}");
                psvimg = Process.Start(new ProcessStartInfo
                {
                    FileName = "psvimg-create",
                    Arguments = $"-m \"{curtentDir}/Homebrews/{homebrew}/{item}.psvmd-dec\" -K {key} \"{curtentDir}/Homebrews/{homebrew}/{item}\" \"{curtentDir}/temp/{aid}/{homebrew}/{item}\""
                });
                await psvimg.WaitForExitAsync();
                psvimg = new Process();
            }
            psvimg = Process.Start(new ProcessStartInfo
            {
                FileName = "cp",
                Arguments = $"-r \"{curtentDir}/Homebrews/{homebrew}/sce_sys\" \"{curtentDir}/temp/{aid}/{homebrew}/\""
            });
            await psvimg.WaitForExitAsync();
            psvimg.Dispose();
            return true;
        }


        //https://stackoverflow.com/a/51514527
        private void CreateEntryFromAny(ZipArchive archive, string sourceName, string entryName = "")
        {
            var fileName = Path.GetFileName(sourceName);
            if (System.IO.File.GetAttributes(sourceName).HasFlag(FileAttributes.Directory))
            {
                CreateEntryFromDirectory(archive, sourceName, Path.Combine(entryName, fileName));
            }
            else
            {
                archive.CreateEntryFromFile(sourceName, Path.Combine(entryName, fileName), CompressionLevel.Fastest);
            }
        }

        private void CreateEntryFromDirectory(ZipArchive archive, string sourceDirName, string entryName = "")
        {
            string[] files = Directory.GetFiles(sourceDirName).Concat(Directory.GetDirectories(sourceDirName)).ToArray();
            foreach (var file in files)
            {
                CreateEntryFromAny(archive, file, entryName);
            }
        }
    }
}
