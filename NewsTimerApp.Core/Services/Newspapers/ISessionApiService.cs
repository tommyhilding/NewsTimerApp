using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Newspapers;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Newspapers
{
    public interface ISessionApiService
    {
        Task<ApiResponse> SaveSession();

        void StartSessionTask(Guid newspaperId);

        void StopSessionTask();

        Session Session { get; }
    }
}
