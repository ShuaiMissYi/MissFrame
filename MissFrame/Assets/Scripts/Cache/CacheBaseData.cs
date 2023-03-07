using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CacheBaseData
{

    public int SubStepId;
    public CacheBaseData(int subStepId) 
    {
        SubStepId = subStepId;
    }
}
