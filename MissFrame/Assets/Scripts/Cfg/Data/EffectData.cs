using MissFrame.Cfg;
using MissFrame.Trigger;
using UnityEngine;

//��ʾ����-������
public class EffectData : ExpressionBase
{

    private CfgShowEffectPathData m_CfgEffectData;
    public CfgShowEffectPathData CfgShowEffectPathData => m_CfgEffectData;

    public EffectData(int id) : base(id)
    {
        
    }

    public override void InitCfgData()
    {
        InitCfgData(m_ShowId);
    }

    private void InitCfgData(int id)
    {
        m_CfgEffectData = CfgUtility.GetInstance().CfgTab.TbShowEffectPath.Get(id);
        if (null == CfgShowEffectPathData)
        {
            Debug.LogErrorFormat($"��id��{id}��Ӧ������Ϊ�գ�����");
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
