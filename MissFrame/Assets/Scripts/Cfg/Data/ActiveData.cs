using DG.Tweening;
using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//显示隐藏-数据类
public class ActiveData: ExpressionBase
{

    private CfgActiveData m_CfgActiveData;
    public CfgActiveData CfgActiveData => m_CfgActiveData;

    private GameObject m_Target;

    public ActiveData(int id):base(id)
    {
        Debug.LogFormat($"m_ShowId:   {m_ShowId}");
        InitData();
    }

    //使用该类时，重置一次，以免场景对象丢失
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
            Debug.LogErrorFormat($"该id：{id}对应的配置为空！！！");
            return;
        }
    }

    //初始化entity所需要的数据
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
            //每次运行时，检测是否对象为空，若为空，则重新获取一次
            InitEntityData();
        }
        if (GameUtilits.GameIsNull(m_Target))
        {
            return;
        }
        m_Target.SetActive(CfgActiveData.IsActive);
        Debug.LogFormat($"监听执行完毕：   {CfgActiveData.Desc}");
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
