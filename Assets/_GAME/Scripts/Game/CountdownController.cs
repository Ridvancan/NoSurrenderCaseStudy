using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    public short countdownValue = 30;

    private void OnEnable()
    {
        EventManager.levelStartEvent.AddListener(TimerReset);
    }
    private void OnDisable()
    {
        EventManager.levelStartEvent.RemoveListener(TimerReset);
    }
    private void Start()
    {
        InvokeRepeating("CountdownTimer", 1, 1);
    }
    void TimerReset()
    {
        countdownValue = 30;
        UIHub.Get<IngameUI>().Countdown(countdownValue);
    }
    private void CountdownTimer()
    {
        if (countdownValue > 0)
        {
            countdownValue--;
            UIHub.Get<IngameUI>().Countdown(countdownValue);
        }
        else
        {
            EventManager.levelFailEvent?.Invoke();
            CancelInvoke();
        }
    }
}
