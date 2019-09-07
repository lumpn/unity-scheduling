using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CancellationToken : ICancellationToken
{
    public bool isCanceled { get; private set; }

    public void Cancel()
    {
        isCanceled = true;
    }
}
