using System;
using System.Collections;
using UnityEngine;

public class Powerup
{
    public const string TAG = "Powerup";
    private const float POWERUP_PERIOD = 5.0f;

    public static IEnumerator PowerDownRoutine(Action action)
    {
        yield return new WaitForSeconds(POWERUP_PERIOD);
        action.Invoke();
    }
}
