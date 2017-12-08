using B4.PE3.DellobelI.Domain.Models;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Abstract
{
    public interface IAppSettingsService
    {
        Task<AppSettings> GetSettings();
        Task SaveSettings(AppSettings settings);
    }
}
