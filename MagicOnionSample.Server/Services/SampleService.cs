﻿using MagicOnion;
using MagicOnion.Server;
using MagicOnionSample.Shared.Services;

namespace MagicOnionSample.Server.Services
{
    public class SampleService : ServiceBase<ISampleService>, ISampleService
    {
        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Logger.Debug($"SumAsync Received:{x}, {y}");
            return x + y;
        }

        public async UnaryResult<int> ProductAsync(int x, int y)
        {
            Logger.Debug($"ProductAsync Received:{x}, {y}");
            return x * y;
        }
    }
}
