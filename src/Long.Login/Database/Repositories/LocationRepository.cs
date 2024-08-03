using Long.Login.Database.Entities;
using System.Net;

namespace Long.Login.Database.Repositories
{
    public static class LocationRepository
    {
        public static CityLocation GetLocation(string ipAddress)
        {
            var address = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(IPAddress.Parse(ipAddress).GetAddressBytes(), 0));
            return GetLocation((uint)address);
        }

        public static CityLocation GetLocation(uint ipAddress)
        {
            using var ctx = new ServerDbContext();
            return ctx.CityLocations.Where(x => ipAddress >= x.IpFrom && ipAddress <= x.IpTo).Take(1).FirstOrDefault();
        }
    }
}
