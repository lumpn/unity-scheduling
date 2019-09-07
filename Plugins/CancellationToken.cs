//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public sealed class CancellationToken : ICancellationToken
{
    public bool isCanceled { get; private set; }

    public void Cancel()
    {
        isCanceled = true;
    }
}
