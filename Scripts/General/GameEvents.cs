using System;

public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnDefeat;
    public static event Action OnVictory;
    public static event Action<int> OnNewEnemyCount;


    public static void RaiseStartGame() => OnStartGame?.Invoke();
    public static void RaiseDefeat() => OnDefeat?.Invoke();
    public static void RaiseVictory() => OnVictory?.Invoke();
    public static void RaiseNewEnemyCount(int count) => OnNewEnemyCount?.Invoke(count);
}