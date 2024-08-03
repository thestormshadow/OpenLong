using Long.Kernel.Managers;
using Long.Kernel.States.User;

namespace Long.Kernel.States.MessageBoxes
{
    public sealed class CaptchaBox : MessageBox
    {
        public CaptchaBox(Character user)
            : base(user)
        {
        }

        public long Value1 { get; private set; }
        public long Value2 { get; private set; }
        public long Result { get; private set; }

        public override Task OnAcceptAsync()
        {
            if (Value1 + Value2 != Result)
            {
                return RoleManager.KickOutAsync(user.Identity, "Wrong captcha reply");
            }

            return Task.CompletedTask;
        }

        public override Task OnCancelAsync()
        {
            if (Value1 + Value2 == Result)
            {
                return RoleManager.KickOutAsync(user.Identity, "Wrong captcha reply");
            }

            return Task.CompletedTask;
        }

        public override Task OnTimerAsync()
        {
            if (expiration.IsActive() && expiration.IsTimeOut())
            {
                return RoleManager.KickOutAsync(user.Identity, "No captcha reply");
            }

            return Task.CompletedTask;
        }

        public async Task GenerateAsync()
        {
            Value1 = await NextAsync(int.MaxValue) % 10;
            Value2 = await NextAsync(int.MaxValue) % 10;
            if (await ChanceCalcAsync(50, 100))
            {
                Result = Value1 + Value2;
            }
            else
            {
                Result = Value1 + Value2 + await NextAsync(int.MaxValue) % 10;
            }

            Message = string.Format(StrBotCaptchaMessage, Value1, Value2, Result);
            TimeOut = 60;

            await SendAsync();
            expiration.Startup(60);
        }
    }
}
