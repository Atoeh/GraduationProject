using System;

public static class GameEvents
{
    public static event Action OnToggleMoveUI;

    public static void ToggleMoveUI()
    {
        OnToggleMoveUI?.Invoke();
    }
}
