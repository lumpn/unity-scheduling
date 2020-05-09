//----------------------------------------
// MIT License
// Copyright(c) 2020 Jonas Boetel
//----------------------------------------
namespace Lumpn.Scheduling
{
    public interface ICancellationToken
    {
        bool isCanceled { get; }
    }
}
