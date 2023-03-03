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
        Debug.LogFormat($"m_ShowId:   {m_ShowId}");
        InitData();
    }

    //使用该类时，重置一次，以免场景对象丢失
    public void InitData()
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
        
        Debug.LogFormat($"监听执行完毕：   {CfgShowEffectPathData.Desc}");
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.ShowEffectPath, this);
    }
    public override void Reset()
    {
        base.Reset();
        EventDispatcher.GetInstance().DispatchEvent(EventType.StopExecuteEffectPath, this);
    }


}
