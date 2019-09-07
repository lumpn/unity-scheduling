# unity-scheduler
A non-allocating Invoke/InvokeRepeating scheduler for Unity

## Installation
Download the entire repository from https://github.com/lumpn/unity-scheduler and put it into your Unity project's *Asset* directory.
For example `MyUnityProject/Assets/ThirdParty/lumpn/unity-scheduler`.

## Usage
```csharp
void DoInvoke()
{
    scheduler.Invoke(InvokedMethod, invokeDelay, this, optionalState, optionalCancellationToken);
}

static void InvokedMethod(object owner, object state)
{
    // ...
}
```

### Notes
* For convencience, create a singleton scheduler. See `SchedulerHost` for details.
* Schedulers should get updated every frame. See `SchedulerHost` for details.