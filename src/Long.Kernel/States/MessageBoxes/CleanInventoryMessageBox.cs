using Long.Kernel.States.User;
using Long.Shared.Helpers;

namespace Long.Kernel.States.MessageBoxes
{
    public sealed class CleanInventoryMessageBox : MessageBox
    {
        private static readonly ILogger logger = Logger.CreateLogger("clear_inventory");

        public CleanInventoryMessageBox(Character user)
            : base(user)
        {
            Message = StrClearInventoryConfirmation;
            TimeOut = 30;
        }

        public override Task OnAcceptAsync()
        {
            logger.Information("User [{0},{1}] has accepted to clean inventory!", user.Identity, user.Name);
            return user.UserPackage.ClearInventoryAsync();
        }
    }
}
