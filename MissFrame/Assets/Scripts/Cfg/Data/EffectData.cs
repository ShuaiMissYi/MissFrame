using DG.Tweening;
using MissFrame.Cfg;
using MissFrame.Trigger;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//显示隐藏-数据类
public class EffectData : ExpressionBase
{

    private CfgShowEffectPathData m_CfgEffectData;
    public CfgShowEffectPathData CfgShowEffectPathData => m_CfgEffectData;

    public EffectData(int id) : base(id)
    {
        
    }

    //使用该类时，重置一次，以免场景对象丢失
    public override void InitCfgData()
    {
        InitCfgData(m_ShowId);
    }

    private void InitCfgData(int id)
    {
        m_CfgEffectData = CfgUtility.GetInstance().CfgTab.TbShowEffectPath.Get(id);
        if (null == CfgShowEffectPathData)
        {
            Debug.LogErrorFormat($"该id：{id}对应的配置为空！！！");
            return;
        }
    }

    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.ShowEffectPath, this);
    }
    public override void Reset()
    {
        base.Reset();
        ResetStep(EventType.StopExecuteEffectPath);
    }


}
