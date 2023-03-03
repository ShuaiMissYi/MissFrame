using DG.Tweening;
using MissFrame.Cfg;
using MissFrame.Trigger;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//��ʾ����-������
public class EffectData : ExpressionBase
{

    private CfgShowEffectPathData m_CfgEffectData;
    public CfgShowEffectPathData CfgShowEffectPathData => m_CfgEffectData;

    public EffectData(int id) : base(id)
    {
        Debug.LogFormat($"m_ShowId:   {m_ShowId}");
        InitData();
    }

    //ʹ�ø���ʱ������һ�Σ����ⳡ������ʧ
    public void InitData()
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
        
        Debug.LogFormat($"����ִ����ϣ�   {CfgShowEffectPathData.Desc}");
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.ShowEffectPath, this);
    }
    public override void Reset()
    {
        base.Reset();
        EventDispatcher.GetInstance().DispatchEvent(EventType.StopExecuteEffectPath, this);
    }


}
