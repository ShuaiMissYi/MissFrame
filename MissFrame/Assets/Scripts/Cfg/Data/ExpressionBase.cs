using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionBase
{
    //����id
    protected int m_ShowId;
    //�ñ���ö������
    protected StepShowType showType;
    //�Ӳ���������
    private SubStepData m_SubStepData;
    public SubStepData SubStep => m_SubStepData;


    public ExpressionBase(int id)
    {
         this.m_ShowId = id;
        InitCfgData();
    }

    public virtual void InitCfgData() { }

    //���б���Ч��
    public virtual void Run(SubStepData data) 
    {
        m_SubStepData = data;
        //�ж��Ƿ�����Ҫ���õĲ���
        ResetOtherSubStep();
    }

    //���������Ӳ�������
    private void ResetOtherSubStep()
    {
        List<int> resetIdList = m_SubStepData.CfgSubStepData.ResetSubStepId;
        foreach (int id in resetIdList)
        {
            LogUtilits.LogFormat($"���������������ݣ�id��{id}");
            SubStepData subStepData = CfgManager.GetInstance().GetSubStepData(id);
            ExpressionBase baseData = subStepData.GetExpressData();
            baseData.Reset();
        }
    }

    //���ñ���Ч��
    public virtual void Reset() { }
   

    protected void ResetStep(EventType eventType)
    {
        //���øò�������б���
        EventDispatcher.GetInstance().DispatchEvent(eventType, this);
    }

}
