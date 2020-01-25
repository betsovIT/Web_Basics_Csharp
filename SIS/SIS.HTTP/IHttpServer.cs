using System;

namespace SIS.HTTP
{
    public interface IHttpServer
    {
        void Start();

        void Reset();

        void Stop();
    }
}
