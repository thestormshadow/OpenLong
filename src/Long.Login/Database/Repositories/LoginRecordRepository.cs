using Long.Login.Database.Entities;
using Long.Login.Network.Login;

namespace Long.Login.Database.Repositories
{
    public class LoginRecordRepository
    {
        protected LoginRecordRepository()
        {
        }

        private static readonly ILogger Logger = Log.ForContext<LoginRecordRepository>();

        public static async Task LoginRcdAsync(LoginClient actor, CityLocation location, bool success)
        {
            try
            {
                await using ServerDbContext ctx = new();
                await ctx.GameAccountLoginRecords.AddAsync(new GameAccountLoginRecord
                {
                    AccountId = (int)actor.AccountID,
                    IpAddress = actor.IpAddress,
                    LocationId = location?.Id ?? 0,
                    LoginTime = DateTime.Now,
                    Success = success
                });
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Could not save login record! {0}", ex.Message);
            }
        }
    }
}