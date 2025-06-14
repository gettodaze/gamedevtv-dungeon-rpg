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
        Log("Start");
        float timeToUse = timeSeconds ?? duration;
        if (!timer.IsStopped() || isStarted)
        {
            throw new InvalidOperationException($"{owner.Name} Timer is already running.");
        }
        timer.Timeout += OnTimeout;
        timer.Start(timeToUse);
        isStarted = true;
    }

    public void Stop()
    {
        Log("Stop");
        if (!isStarted)
        {
            throw new InvalidOperationException($"{owner.Name} timer is not running.");
        }
        timer.Stop();
        timer.Timeout -= OnTimeout;
        isStarted = false;
    }

    private void OnTimeout()
    {
        Log("OnTimeout");
        onTimeout?.Invoke();
        if (oneShot) Stop();
    }

    public void Log(object msg)
    {
        string tag = "TimerHelper";
        if (owner is CharacterState)
        {
            tag = $"{(owner as CharacterState).characterNode.Name} {tag}";
        }
        GD.Print($"{owner.Name}: [{tag}] {msg}");
    }
}