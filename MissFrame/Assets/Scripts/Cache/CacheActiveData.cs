using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheActiveData : CacheBaseData
{

    private GameObject m_Target;
    public GameObject Target=>m_Target;

    public CacheActiveData(int subStepId) : base(subStepId)
    {
    }

    public void SetTarget(GameObject target)
    {
        m_Target = target;  
    }




}
