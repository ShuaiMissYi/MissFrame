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
    }

    //���ñ���Ч��
    public virtual void Reset()
    {
        
    }

}
