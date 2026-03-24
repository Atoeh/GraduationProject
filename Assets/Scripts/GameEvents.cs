using System;

public static class GameEvents
{
    //should change the name of this dogwash to onMOve bcs that is the damm thing that happens
    public static event Action OnToggleMoveUI;
    // what is the use for this, basicly same as a button but through code?>\]
    public static event Action OnInteract;
    public static event Action<float> OnChangeDarkness;
    public static void ToggleMoveUI()
    {
        OnToggleMoveUI?.Invoke();
    }

    public static void ChangeDarkness(float value)
    { 
        OnChangeDarkness?.Invoke(value);
    }
}