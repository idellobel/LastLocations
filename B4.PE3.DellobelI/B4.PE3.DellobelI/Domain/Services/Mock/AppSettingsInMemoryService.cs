using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Threading.Tasks;
using B4.PE3.DellobelI.Domain.Models;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class AppSettingsInMemorySQLiteService : IAppSettingsService
    {
        private static AppSettings currentSettings = new AppSettings
        {
            CurrentUserId = Guid.Empty, //refers to (first) local user
            EnableListSharing = true,
            EnableNotifications = false
        };

        public async Task<AppSettings> GetSettings()
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            return currentSettings;
        }

            public async Task SaveSettings(AppSettings settings)
        {
                await Task.Delay(Constant.Mocking.FakeDelay);
                currentSettings = settings;
        }
    }
}
