﻿namespace Hangfire.Services
{
    public interface IJobTestService
    {
        void FireAndForgetJob();

        void ReccuringJob();

        void DelayedJob();

        void ContinuationJob();

    }
}
