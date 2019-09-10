# unity-scheduler
A non-allocating Invoke/InvokeRepeating scheduler for Unity

## Installation
Download the entire repository from https://github.com/lumpn/unity-scheduler and put it into a subdirectory of your Unity project's *Asset* directory.
For example `MyUnityProject/Assets/Plugins/lumpn/unity-scheduler`.

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
* The invoked method should be `static` to avoid capturing the `this` reference, which would generate garbage.
* For non-static methods, you can pass `this` via the `owner` parameter.
* Optionally additional state can be passed via the `state` parameter.
* See `InvokeDemo` for details.
