using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : Singleton<StepController>
{
    //��ǰ��������
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

    //���ò���
    public void ResetStep()
    {
        if (GameUtilits.GameIsNull(m_CurStepData,false))
        {
            Debug.Log("��ǰ�޲�������");
            return;
        }
        m_CurStepData.Reset();
    }





}
