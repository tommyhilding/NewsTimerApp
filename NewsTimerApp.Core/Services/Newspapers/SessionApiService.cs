using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Newspapers;

namespace NewsTimerApp.Core.Services.Newspapers
{
    public class SessionApiService : BaseApiService, ISessionApiService
    {
        private Session _session = null;
        private Task _sessionTask = null;
        private CancellationTokenSource _sessionCancelTokenSource = new CancellationTokenSource();

        public SessionApiService(ISecureStorageService secureStorageService) : base(secureStorageService)
        {

        }

        public Session Session 
        { 
            get 
            {
                return _session;
            }
        }

        public async Task<ApiResponse> SaveSession()
        {
            SessionRequest request = new SessionRequest();
            request.DurationInSeconds = Session.Duaration;
            request.NewspaperId = Session.NewspaperId;
            request.Start = Session.Start;
            return await PostAsync<ApiResponse>("userlog/savesession", request);
        }

        public void StartSessionTask(Guid newspaperId)
        {
            if(_session == null)
            {
                _session = new Session();
                _session.NewspaperId = newspaperId;
            }

            _session.Start = DateTime.Now;
            _session.Duaration = 0;

            this.SessionTask(_sessionCancelTokenSource.Token);
        }

        public void StopSessionTask()
        {
            _sessionCancelTokenSource.Cancel();
        }

        private Task SessionTask(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                bool running = true;

                while (running == true)
                {
                    try
                    {
                        await Task.Delay(1000, cancellationToken);

                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        _session.Duaration += 1;

                        Debug.WriteLine("###### Session Task Duration =" + _session.Duaration);
                    }
                    catch (Exception ex)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            Debug.WriteLine("###### Session Task stopped via a cancel request");
                            running = false;
                        }
                        else
                        {
                            Debug.WriteLine("Session Task throwed an Exception (but still running): " + ex.Message);
                        }
                    }
                }
            });
        }
    }
}
