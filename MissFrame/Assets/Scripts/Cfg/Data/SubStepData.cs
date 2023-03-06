using MissFrame.Cfg;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubStepData
{
    //子步骤配置数据
    private CfgSubStepData m_CfgSubStepData;
    public CfgSubStepData CfgSubStepData => m_CfgSubStepData;

    private StepData m_StepData;
    public StepData MainStepData => m_StepData;

    //子步骤是否完成
    private bool m_IsFinish = false;
    public bool IsFinish => m_IsFinish;

    //子步骤是否开始
    private bool m_IsRuning = false;
    public bool IsRuning => m_IsRuning;

    public SubStepData(int id)
    {
        InitCfgData(id);
    }

    public void InitCfgData(int id)
    {
        m_CfgSubStepData = CfgUtility.GetInstance().CfgTab.TbSubStep.Get(id);
        if (null == m_CfgSubStepData)
        {
            Debug.LogErrorFormat($"该子步骤id：{id}对应的子步骤配置为空！！！");
            return;
        }
    }

    //关联主步骤
    public void RelevanceStep(StepData stepData)
    {
        m_StepData = stepData;
    }

    //方法内部判断自身是否可以运行
    public void Run()
    {
        //判断是否有前置步骤
        bool isHave = IsHavePreposition();
        if (isHave)
        {
            //前置步骤是否完成
            bool isFinish = PrepositionIsFinish();
            if (!isFinish)
            {
                return;
            }
        }
        //判断自身是否已完成
        if (IsFinish)
        {
            //执行子步骤
            foreach (var id in m_CfgSubStepData.NextStepIdList)
            {
                SubStepData data = CfgManager.GetInstance().GetSubStepData(id);
                data.Run();
            }
        }
        else
        {
            //EventDispatcher.GetInstance().DispatchEvent();
            ExpressionBase data = CfgManager.GetInstance().GetExpressInfo (m_CfgSubStepData.TriggerType, m_CfgSubStepData.TriggerId);
            if (null!=data)
            {
                m_IsRuning = true;
                data.Run(this);
            }
        }
    }


    /// <summary>
    /// 是否有前置步骤
    /// </summary>
    /// <returns></returns>
    public bool IsHavePreposition()
    {
        return m_CfgSubStepData.PrepositionStepIdList.Count > 0;
    }
    /// <summary>
    /// 前置步骤是否完成
    /// </summary>
    /// <returns></returns>
    public bool PrepositionIsFinish()
    {
        if (!IsHavePreposition())
        {
            return true;
        }
        List<int> idList = GetPrepositionIdList();
        bool isFinish = true;
        for (int i = 0; i < idList.Count; i++)
        {
            SubStepData data = CfgManager.GetInstance().GetSubStepData(idList[i]);
            if (!data.IsFinish)
            {
                isFinish = false;
                break;
            }
        }
        return isFinish;
    }

    /// <summary>
    /// 获取前置步骤id列表
    /// </summary>
    /// <returns></returns>
    public List<int> GetPrepositionIdList()
    {
        return m_CfgSubStepData.PrepositionStepIdList;
    }

    /// <summary>
    /// 步骤完成
    /// </summary>
    public void FinishStep()
    {
        m_IsFinish = true;
        Debug.LogFormat($"步骤完成   {m_CfgSubStepData.Desc}");
    }
    

    //重置子步骤
    public void ResetSubStep()
    {
        //重置子步骤对象：表现、成状态
        if (IsRuning)
        {
            //如果正在运行，则重置
            ExpressionBase data = CfgManager.GetInstance().GetExpressInfo(m_CfgSubStepData.TriggerType, m_CfgSubStepData.TriggerId);
            if (null != data)
            {
                data.Reset();
            }
        }
        ResetStepState();
    }
    /// <summary>
    /// 重置步骤状态 
    /// </summary>
    private void ResetStepState()
    {
        m_IsFinish = false;
        m_IsRuning = false;
    }







}
