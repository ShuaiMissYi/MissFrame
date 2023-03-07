using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightData : ExpressionBase
{

    private CfgHighlightData m_CfgHighlightData;
    public CfgHighlightData CfgHighlightData => m_CfgHighlightData;

    public HighlightData(int id) : base(id)
    {
    }

    public override void InitCfgData()
    {
        base.InitCfgData();
        m_CfgHighlightData = CfgUtility.GetInstance().CfgTab.TbHighlight.Get(m_ShowId);
        if (null == m_CfgHighlightData)
        {
            Debug.LogErrorFormat($"��id��{m_ShowId}��Ӧ������Ϊ�գ�����");
            return;
        }
    }

    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.Highlight, this);
    }

    public override void Reset()
    {
        base.Reset();
        ResetStep(EventType.StopHightHighlight);
    }








}
