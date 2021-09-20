//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
namespace Lumpn.Scheduling
{
    public interface ICancellationToken
    {
        bool isCanceled { get; }
    }
}
