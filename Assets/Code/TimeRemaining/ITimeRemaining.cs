using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeRemaining 
{
    Action Method { get; }
    bool IsRepeating { get; }
    float Time { get; }
    float CurrentTime { get; set; }
}
