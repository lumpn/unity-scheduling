# unity-scheduler
A non-allocating Invoke/InvokeRepeating scheduler for Unity

## Installation
Download the entire repository from https://github.com/lumpn/unity-scheduler and use Unity's built in package manager to [add package from disk](https://docs.unity3d.com/Manual/upm-ui-local.html).

## Usage

### Static method
```csharp
class Wizard
{
    void CastSpell()
    {
        scheduler.Invoke(ApplyDamage, castDelay);
    }

    static void ApplyDamage(object owner, object state)
    {
        // apply damage here
    }
}
```

### Member method
```csharp
class Wizard
{
    void CastSpell()
    {
        scheduler.Invoke(ApplyDamage, castDelay, this);
    }

    // NOTE: we are using a static method to avoid temporarily
    // allocating memory for a closure over the `this` reference.
    static void ApplyDamage(object owner, object state)
    {
        var wizard = (Wizard)owner;
        wizard.ApplyDamage();
    }
    
    void ApplyDamage()
    {
        // apply damage here
    }
}
```

### Passing state
```csharp
class Wizard
{
    void CastSpell()
    {
        scheduler.Invoke(ApplyDamage, castDelay, this, targetEnemy);
    }

    static void ApplyDamage(object owner, object state)
    {
        var wizard = (Wizard)owner;
        var ememy = (Enemy)state;
        wizard.ApplyDamage(state);
    }
    
    void ApplyDamage(Enemy enemy)
    {
        // apply damage to enemy here
    }
}
```

### Cancellation token
```csharp
class Wizard
{
    private ICancellationToken castToken;

    void CastSpell()
    {
        castToken = new CancellationToken();
        scheduler.Invoke(ApplyDamage, castDelay, this, targetEnemy, castToken);
    }
    
    void CancelCast()
    {
        castToken.Cancel();
    }
    
    // NOTE: will never get called if the token gets
    // cancelled before the cast delay is over.
    static void ApplyDamage(object owner, object state)
    {
        // ...
    }
}
```

## Notes
* For convencience, create a singleton scheduler. See `SchedulerHost` for details.
* Schedulers should get updated every frame. See `SchedulerHost` for details.
* The invoked method should be `static` to avoid capturing the `this` reference, which would generate garbage.
* For non-static methods, add a `static` method and pass `this` via the `owner` parameter.
  Unfortunately this kind of boilerplate is the only way to avoid generating garbage.
* Optionally additional state can be passed via the `state` parameter.
* See `InvokeDemo` for details.
