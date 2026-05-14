using System;
using Unity.VisualScripting;

public static class GameEvents
{
    //should change the name of this dogwash to onMOve bcs that is the damm thing that happens
    public static event Action OnToggleMoveUI;
    public static event Action<float> OnChangeDarkness;
    public static event Action<float> OnLootPickUp;
    public static event Action<float> OnQuotaSet;
    public static event Action OnQuotaHit;
    public static event Action OnOpenDoor; //Exit door even, misschien andere naam?
    public static event Action OnExitLevel;
    public static event Action OnCandleDepleted;


    public static void ToggleMoveUI()
    {
        OnToggleMoveUI?.Invoke();
    }

    public static void ChangeDarkness(float value)
    { 
        OnChangeDarkness?.Invoke(value);
    }

    public static void LootPickUp(float value)
    {
        OnLootPickUp?.Invoke(value);
    }

    public static void QuotaSet(float value)
    { 
        OnQuotaSet?.Invoke(value);
    }

    public static void QuotaHit()
    {
        OnQuotaHit?.Invoke();
    }

    public static void OpenDoor()
    { 
        OnOpenDoor?.Invoke();
    }

    public static void Exitevel()
    {
        OnExitLevel?.Invoke();
    }

    public static void CandleDepleted()
    {
        OnCandleDepleted?.Invoke();
    }
}