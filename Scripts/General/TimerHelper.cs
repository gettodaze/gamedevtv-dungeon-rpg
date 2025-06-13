using Godot;
using System;

public class TimerHelper
{
    private readonly Timer timer;
    private float duration;
    private Action onTimeout;
    private readonly Node owner;
    private readonly bool oneShot;
    private bool isStarted = false;

    public TimerHelper(Node owner, Action onTimeout, float duration = 1.0f, bool oneShot = false)
    {
        this.owner = owner;
        this.onTimeout = onTimeout;
        this.duration = duration;
        this.oneShot = oneShot;
        timer = new Timer();
        owner.AddChild(timer);

    }

    public void Start(float? timeSeconds = null)
    {
        float timeToUse = timeSeconds ?? duration;
        if (!timer.IsStopped() || isStarted)
        {
            throw new InvalidOperationException($"{owner.Name} Timer is already running.");
        }
        timer.Timeout += OnTimeout;
        timer.Start(timeToUse);
    }

    public void Stop()
    {
        if (!isStarted)
        {
            throw new InvalidOperationException($"{owner.Name} timer is not running.");
        }
        timer.Stop();
        timer.Timeout -= OnTimeout;
        onTimeout = null;
    }

    private void OnTimeout()
    {
        onTimeout?.Invoke();
        if (oneShot) Stop();
    }
}