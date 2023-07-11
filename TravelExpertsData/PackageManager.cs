using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class PackageManager
    {
        // Get all packages available ordered using the PkgName column (Coded by: Muhammad & Ali)
        public static List<Package> GetAllPackages()
        { 
            TravelExpertsContext db=new TravelExpertsContext();
            List<Package> packages = db.Packages.OrderBy(o => o.PkgName).ToList();

            return packages ;
        }
    }
}
