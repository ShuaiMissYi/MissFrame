using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMoveData : ExpressionBase
{

    private CfgTweenMoveData m_CfgTweenMoveData;
    public CfgTweenMoveData CfgTweenMoveData=>m_CfgTweenMoveData;

    public TweenMoveData(int id) : base(id)
    {

    }

    public override void InitCfgData()
    {
        base.InitCfgData();
        m_CfgTweenMoveData = CfgUtility.GetInstance().CfgTab.TbTweenMove.Get(m_ShowId);
        if (null == m_CfgTweenMoveData)
        {
            Debug.LogErrorFormat($"��id��{m_ShowId}��Ӧ������Ϊ�գ�����");
            return;
        }
    }

    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.TweenMove, this);
    }

    public override void Reset()
    {
        base.Reset();
        EventDispatcher.GetInstance().DispatchEvent(EventType.StopTweenMove, this);
    }


}
