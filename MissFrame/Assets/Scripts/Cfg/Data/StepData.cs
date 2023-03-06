using MissFrame.Cfg;
using UnityEngine;

public class StepData
{
    //步骤配置数据
    private CfgStepData m_CfgStepData;

    public CfgStepData CfgStepData { get => m_CfgStepData; }

    //子步骤总数
    public int AllSubStepNun => CfgStepData.ChildStepIdList.Count;
    //当前已完成的子步骤数
    private int m_CurFinishSubStepNum = 0;


    //步骤是否完成
    private bool m_IsFinish = false;
    public bool IsFinish=>m_IsFinish;
   

    public StepData(int stepId)
    {
        InitCfgData(stepId);
        EventDispatcher.GetInstance().Regist(EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //初始化配置数据
    public void InitCfgData(int stepId)
    {
        //步骤配置
        m_CfgStepData = CfgUtility.GetInstance().CfgTab.TbStep.Get(stepId);
        if (null == m_CfgStepData)
        {
            Debug.LogErrorFormat($"该步骤id：{stepId}对应的步骤配置为空！！！");
            return;
        }
    }
    //设置子步骤关联
    public void InitSubStepDataRelevance()
    {
        foreach (int id in m_CfgStepData.ChildStepIdList)
        {
            SubStepData subStep = CfgManager.GetInstance().GetSubStepData(id);
            subStep.RelevanceStep(this);
        }
    }


    public void Start()
    {
        Debug.Log("开始执行");
        RunStep();
    }

    public void Reset()
    {
        Debug.Log("停止执行");
        ResetStep();
    }

    public void Dispos()
    {
        EventDispatcher.GetInstance().UnRegist(EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //执行子步骤流程
    private void RunStep()
    {
        foreach (int id in CfgStepData.ChildStepIdList)
        {
            SubStepData data = CfgManager.GetInstance().GetSubStepData(id);
            if (!data.IsHavePreposition())
            {
                data.Run();
            }
        }
    }

    //重置子步骤流程
    private void ResetStep()
    {
        foreach (int id in CfgStepData.ChildStepIdList)
        {
            SubStepData data = CfgManager.GetInstance().GetSubStepData(id);
            data.ResetSubStep();
        }
        m_IsFinish = false;
        m_CurFinishSubStepNum = 0;
    }

    private void OnFinishSubStepCallBack(params object[] objs)
    {
        SubStepData subStep = objs[0]as SubStepData;
        if (subStep.MainStepData.CfgStepData.StepId != CfgStepData.StepId)
        {
            return;
        }
        m_CurFinishSubStepNum += 1;
        if (m_CurFinishSubStepNum==AllSubStepNun)
        {
            //子步骤全部执行完毕
            m_IsFinish = true;
            LogUtilits.LogFormat("子步骤全部执行完毕");
            EventDispatcher.GetInstance().DispatchEvent(EventType.FinishStep,this);
        }
    }


}
