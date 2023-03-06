using DG.Tweening;
using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//��ʾ����-������
public class ActiveData: ExpressionBase
{

    private CfgActiveData m_CfgActiveData;
    public CfgActiveData CfgActiveData => m_CfgActiveData;

    public ActiveData(int id):base(id)
    {
        
    }

    //ʹ�ø���ʱ������һ�Σ����ⳡ������ʧ
    public override void InitCfgData()
    {
        InitCfgData(m_ShowId);
    }

    private void InitCfgData(int id)
    {
        m_CfgActiveData = CfgUtility.GetInstance().CfgTab.TbActive.Get(id);
        if (null == CfgActiveData)
        {
            Debug.LogErrorFormat($"��id��{id}��Ӧ������Ϊ�գ�����");
            return;
        }
    }


    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.Active, this);
    }
    public override void Reset()
    {
        base.Reset();
        EventDispatcher.GetInstance().DispatchEvent(EventType.StopExecuteActive, this);
    }


}
