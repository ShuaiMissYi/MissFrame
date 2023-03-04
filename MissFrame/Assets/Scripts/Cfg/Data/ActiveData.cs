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

    private GameObject m_Target;

    public ActiveData(int id):base(id)
    {
        InitData();
    }

    //ʹ�ø���ʱ������һ�Σ����ⳡ������ʧ
    public void InitData()
    {
        InitCfgData(m_ShowId);
        InitEntityData();
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

    //��ʼ��entity����Ҫ������
    private void InitEntityData()
    {
        if (null==CfgActiveData)
        {
            return;
        }
        GameObject root = GameObject.Find(CfgActiveData.RootName);
        if (GameUtilits.GameIsNull(root))
        {
            return;
        }
        GameObject target = root.transform.Find(CfgActiveData.RelativePath).gameObject;
        if (GameUtilits.GameIsNull(target))
        {
            return;
        }
        m_Target = target;
    }


    public override void Run(SubStepData data)
    {
        base.Run(data);
        if (GameUtilits.GameIsNull(m_Target,false))
        {
            //ÿ������ʱ������Ƿ����Ϊ�գ���Ϊ�գ������»�ȡһ��
            InitEntityData();
        }
        if (GameUtilits.GameIsNull(m_Target))
        {
            return;
        }
        m_Target.SetActive(CfgActiveData.IsActive);
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, SubStep);
    }
    public override void Reset()
    {
        base.Reset();
        if (GameUtilits.GameIsNull(m_Target))
        {
            return;
        }
        m_Target.SetActive(!CfgActiveData.IsActive);
    }


}
