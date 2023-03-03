using MissFrame.Cfg;
using UnityEngine;

public class StepData
{
    //步骤配置数据
    private CfgStepData m_CfgStepData;

    public CfgStepData CfgStepData { get => m_CfgStepData; }

    public StepData(int stepId)
    {
        InitCfgData(stepId);
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


    public void Start()
    {
        //1.开始执行
        //Debug.LogFormat($"{m_CfgStepData.StepId} 发送开始执行监听");
        //EventDispatcher.GetInstance().DispatchEvent( EventType.StartExecuteStep,this);
        Debug.Log("开始执行");
        RunStep();
    }

    public void Reset()
    {
        //1.停止步骤代码逻辑
        //Debug.LogFormat($"{m_CfgStepData.StepId}发送停止执行监听");
        Debug.Log("停止执行");
        ResetStep();
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
    }


}
