using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProject.Foundation.SitecoreExtensions.Helpers
{
    public static class LocationHelper
    {
        public static double GetDistance(GeoCoordinate src, GeoCoordinate dest, bool isMetric)
        {
            // GetDistanceTo returns distance in meters
            return isMetric
                ? Math.Round(src.GetDistanceTo(dest), 2)
                : Math.Round(src.GetDistanceTo(dest) / 1000, 2);
        }
        public static double GetDistance(double latSrc, double lonSrc, double latDest, double lonDest, bool isMetric)
        {
            var src = new GeoCoordinate(latSrc, lonSrc);
            var des = new GeoCoordinate(latDest, lonDest);
            return GetDistance(src, des, isMetric);        
        }
    }
}
