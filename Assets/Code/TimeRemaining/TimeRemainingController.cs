using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRemainingController : IExecute
{
    private readonly List<ITimeRemaining> _timeRemainings;

    public bool IsActive { get; private set; }

    public TimeRemainingController()
    {
        _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        IsActive = true;
    }
    public void Execute()
    {
        var time = Time.deltaTime;
        for(int i=0;i<_timeRemainings.Count; i++)
        {
            var obj = _timeRemainings[i];
            obj.CurrentTime -= time;
            if (obj.CurrentTime<=0.0f)
            {
                if(!obj.IsRepeating)
                {
                    obj.DeleteTimeRemaining();
                }
                else
                {
                    obj.CurrentTime = obj.Time;
                }
                obj?.Method?.Invoke();
            }
        }
    }
}
