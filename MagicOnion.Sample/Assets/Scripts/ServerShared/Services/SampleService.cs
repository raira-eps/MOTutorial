﻿using MagicOnion;

namespace MagicOnionSample.Shared.Services
{
    public interface ISampleService : IService<ISampleService>
    {
        UnaryResult<int> SumAsync(int x, int y);
        UnaryResult<int> ProductAsync(int x, int y);
    }
}

