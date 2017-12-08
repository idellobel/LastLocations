using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.SQLite
{
    class AppSettingsSQLiteService : SQLiteServiceBase, IAppSettingsService
    {
        public async Task<AppSettings> GetSettings()
        {
            return await Task.Run<AppSettings>(async () =>
            {
                try
                {
                    int numSettings = connection.Table<AppSettings>().Count();
                    if (numSettings == 0)
                    {
                        await SaveSettings(new AppSettings
                        {
                            CurrentUserId = Guid.Empty,
                            EnableListSharing = true,
                            EnableNotifications = false
                        });
                    }
                    return connection.Table<AppSettings>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });

        }

        public async Task SaveSettings(AppSettings settings)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplace(settings);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
