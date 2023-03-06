using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenLookAtData : ExpressionBase
{

    private CfgTweenLookAtData m_CfgTweenLookAtData;
    public CfgTweenLookAtData CfgTweenLookAtData => m_CfgTweenLookAtData;

    public TweenLookAtData(int id) : base(id)
    {

    }

    public override void InitCfgData()
    {
        base.InitCfgData();
        m_CfgTweenLookAtData = CfgUtility.GetInstance().CfgTab.TbTweenLookAt.Get(m_ShowId);
        if (null == m_CfgTweenLookAtData)
        {
            Debug.LogErrorFormat($"该id：{m_ShowId}对应的配置为空！！！");
            return;
        }
    }

    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.TweenLookAt, this);
    }

    public override void Reset()
    {
        base.Reset();
        ResetStep(EventType.StopTweenLookAt);
    }









}
