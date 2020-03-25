using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models.Newspapers;

namespace NewsTimerApp.Core.Services.Newspapers
{
    public class NewspaperApiService : BaseApiService, INewspaperApiService
    {
        public NewspaperApiService(ISecureStorageService secureStorageService) : base(secureStorageService)
        {

        }

        public async Task<NewspaperResponse> GetNewspapers()
        {
            var response = await GetAsync<NewspaperResponse>("newspaper/getnewspapers");
            return response ?? new NewspaperResponse();
        }

        public async Task<NewspaperCookieResponse> GetNewspaperCookies(Guid newspaperid)
        {
            var cookies = await GetAsync<NewspaperCookieResponse>("newspaper/getnewspapercookies?newspaperId=" + newspaperid.ToString());
            return cookies ?? new NewspaperCookieResponse();
        }
    }
}
