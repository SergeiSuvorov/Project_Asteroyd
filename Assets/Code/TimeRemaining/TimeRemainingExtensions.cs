using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeRemainingExtensions 
{
    private static readonly List<ITimeRemaining> _timeRemainings = new List<ITimeRemaining>();
    public static List<ITimeRemaining> TimeRemainings => _timeRemainings;

    public static void AddTimeRemaining(this ITimeRemaining value)
    {
        if (_timeRemainings.Contains(value))
        {
            return;
        }
        Debug.Log("Add");
        value.CurrentTime = value.Time;
        _timeRemainings.Add(value);
    }

    public static void DeleteTimeRemaining(this ITimeRemaining value)
    {
        if (!_timeRemainings.Contains(value))
        {
            return;
        }
        Debug.Log("Remove");
        _timeRemainings.Remove(value);
    }
}
