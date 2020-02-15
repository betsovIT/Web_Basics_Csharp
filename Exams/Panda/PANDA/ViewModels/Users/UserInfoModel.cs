using PandaWebApp.ViewModels.Packages;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Users
{
    public class UserInfoModel
    {
        public string Username { get; set; }

        public int Role { get; set; }

        public ICollection<PackageInfoViewModel> PendingPackages { get; set; }

        public ICollection<PackageInfoViewModel> ShippedPackages { get; set; }

        public ICollection<PackageInfoViewModel> DeliveredPackages { get; set; }
    }
}
