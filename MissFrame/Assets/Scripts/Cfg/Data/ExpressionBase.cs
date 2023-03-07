using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionBase
{
    //表现id
    protected int m_ShowId;
    //该表现枚举类型
    protected StepShowType showType;
    //子步骤数据类
    private SubStepData m_SubStepData;
    public SubStepData SubStep => m_SubStepData;


    public ExpressionBase(int id)
    {
         this.m_ShowId = id;
        InitCfgData();
    }

    public virtual void InitCfgData() { }

    //运行表现效果
    public virtual void Run(SubStepData data) 
    {
        m_SubStepData = data;
        //判断是否有需要重置的步骤
        ResetOtherSubStep();
    }

    //重置其他子步骤数据
    private void ResetOtherSubStep()
    {
        List<int> resetIdList = m_SubStepData.CfgSubStepData.ResetSubStepId;
        foreach (int id in resetIdList)
        {
            LogUtilits.LogFormat($"重置其他步骤数据：id：{id}");
            SubStepData subStepData = CfgManager.GetInstance().GetSubStepData(id);
            ExpressionBase baseData = subStepData.GetExpressData();
            baseData.Reset();
        }
    }

    //重置表现效果
    public virtual void Reset() { }
   

    protected void ResetStep(EventType eventType)
    {
        //重置该步骤的所有表现
        EventDispatcher.GetInstance().DispatchEvent(eventType, this);
    }

}
