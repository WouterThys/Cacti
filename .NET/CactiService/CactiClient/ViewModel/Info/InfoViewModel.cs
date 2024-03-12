using CactiClient.ViewModel.Cactus;
using CactiClient.WebClient;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient.ViewModel.Info
{
    [POCOViewModel]
    public class InfoViewModel : BaseViewModel
    {
        public static InfoViewModel Create()
        {
            return ViewModelSource.Create(() => new InfoViewModel());
        }

        public override string Name => "InfoViewModel";
        public override string Title => "Info";
        public override string ViewName => "InfoView";



        public virtual string? Version { get; protected set; } = "";
        public virtual string? Layout { get; protected set; } = "";
        public virtual string? Address { get; protected set; } = "";

        public virtual string? InstallDirectory { get; protected set; } = ""; 
        public virtual string? CacheDirectory { get; protected set; } = "";

        public virtual string? MachineIp { get; protected set; } = "";


        protected InfoViewModel()
        {

        }

        public override void Load()
        {
            LoadDirectories();

            LoadVersion();

            LoadSettings();

            LoadMachineIp();
        }


        private void LoadDirectories()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            InstallDirectory = Path.GetDirectoryName(strExeFilePath);

            CacheDirectory = FileService.GetInstance().GetBasePath();
        }

        private void LoadVersion()
        {
            try
            {
                if (File.Exists("version.txt"))
                {
                    var content = File.ReadLines("version.txt").ToArray();
                    var version = content[0];
                    var build = content[1];

                    Version = $"{version} - {build}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void LoadSettings()
        {
            try
            {
                Layout = Properties.Settings.Default.Theme;
                Address = Properties.Settings.Default.Address;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void LoadMachineIp() 
        {
            try
            {
                // IP
                var hostname = Dns.GetHostName();
                var host = Dns.GetHostByName(hostname);
                MachineIp = host.AddressList.FirstOrDefault(ip => ip.ToString().StartsWith("192."))?.ToString() ?? "";
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
