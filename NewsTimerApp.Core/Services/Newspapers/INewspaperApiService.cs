using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Newspapers;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Newspapers
{
    public interface INewspaperApiService
    {
        Task<NewspaperResponse> GetNewspapers();

        Task<NewspaperCookieResponse> GetNewspaperCookies(Guid newspaperid);
    }
}
