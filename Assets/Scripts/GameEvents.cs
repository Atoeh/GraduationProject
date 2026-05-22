using System;
using Unity.VisualScripting;

public static class GameEvents
{
    //should change the name of this dogwash to onMOve bcs that is the damm thing that happens
    public static event Action OnToggleMoveUI;
    public static event Action<float> OnInventoryUpdated; //Misschien ook een array erbij voor uitbreiding met item list?
    public static event Action<float> OnChangeDarkness;
    public static event Action<float> OnQuotaSet;

    public static event Action<float> OnLootPickUp;
    public static event Action OnNoLoot;
    public static event Action OnQuotaHit;
    public static event Action<bool> OnOpenDoor; //Exit door even, misschien andere naam?

    public static event Action OnTimerPause;
    public static event Action OnExitLevel; // wat doel hier ook alweer van
    public static event Action OnCandleDepleted; //change name to game over?
    public static event Action OnLeaveGame;

    public static void ToggleMoveUI()
    {
        OnToggleMoveUI?.Invoke();
    }

    public static void InventoryUpdated(float value)
    {
        OnInventoryUpdated?.Invoke(value);
    }

    public static void ChangeDarkness(float value)
    { 
        OnChangeDarkness?.Invoke(value);
    }

    public static void LootPickUp(float value)
    {
        OnLootPickUp?.Invoke(value);
    }

    public static void NoLoot()
    {
        OnNoLoot?.Invoke();
    }

    public static void QuotaSet(float value)
    { 
        OnQuotaSet?.Invoke(value);
    }

    public static void QuotaHit()
    {
        OnQuotaHit?.Invoke();
    }

    public static void OpenDoor(bool isQuotaHit)
    { 
        OnOpenDoor?.Invoke(isQuotaHit);
    }

    public static void TimerPause()
    { 
        OnTimerPause?.Invoke();
    }

    public static void ExitLevel()
    {
        OnExitLevel?.Invoke();
    }

    public static void CandleDepleted()
    {
        OnCandleDepleted?.Invoke();
    }

    public static void LeaveGame()
    { 
        OnLeaveGame?.Invoke();
    }

}