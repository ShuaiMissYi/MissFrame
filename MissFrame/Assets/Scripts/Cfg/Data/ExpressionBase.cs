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
    }

    //重置表现效果
    public virtual void Reset()
    {
        
    }

}
