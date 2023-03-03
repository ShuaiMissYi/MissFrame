using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : Singleton<StepController>
{
    //当前步骤数据
    private StepData m_CurStepData;

    public void StartStep(int stepId)
    {
        if (null!= m_CurStepData)
        {
            m_CurStepData.Reset();
            m_CurStepData = null;
        }
        StepData stepData = CfgManager.GetInstance().GetStepData(stepId);
        if (null != stepData)
        {
            stepData.Start();
            m_CurStepData = stepData;
        }
    }

    //重置步骤
    public void ResetStep()
    {
        if (GameUtilits.GameIsNull(m_CurStepData,false))
        {
            Debug.Log("当前无操作步骤");
            return;
        }
        m_CurStepData.Reset();
    }





}
