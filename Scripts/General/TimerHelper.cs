using Godot;
using System;

public class TimerHelper
{
    private readonly Timer timer;
    private float duration;
    private Action onTimeout;
    private readonly Node owner;
    private readonly bool loop;

    public TimerHelper(Node owner, Action onTimeout, float duration = 1.0f, bool loop = false)
    {
        this.owner = owner;
        this.onTimeout = onTimeout;
        this.duration = duration;
        this.loop = loop;
        timer = new Timer();
        owner.AddChild(timer);

    }

    public void Start(float? timeSeconds = null)
    {
        float timeToUse = timeSeconds ?? duration;
        timer.Start(timeToUse);
        if (!timer.IsStopped())
        {
            throw new InvalidOperationException($"{owner.Name} Timer is already running.");
        }
        timer.Timeout += OnTimeout;
        timer.Start(timeToUse);
    }

    public void Stop()
    {
        timer.Stop();
        timer.Timeout -= OnTimeout;
        onTimeout = null;
    }

    private void OnTimeout()
    {
        onTimeout?.Invoke();
        if (!loop) Stop();
    }
}