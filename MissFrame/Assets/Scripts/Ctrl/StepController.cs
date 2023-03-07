using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StepController : SingletonMono<StepController>
{
    //当前步骤数据
    private StepData m_CurStepData;

    private Coroutine m_CorAutoNext;


    private void Awake()
    {
        EventDispatcher.GetInstance().Regist(EventType.FinishStep, OnFinishStepCallBack);
    }

    //运行步骤
    public void StartStep(int stepId)
    {
        if (null!= m_CurStepData)
        {
            //执行该步骤时，判断上一个步骤是否需要重置
            if (m_CurStepData.CfgStepData.IsNeedReset)
            {
                m_CurStepData.Reset();
                m_CurStepData = null;
            }
        }
        StepData stepData = CfgManager.GetInstance().GetStepData(stepId);
        if (null != stepData)
        {
            m_CurStepData = stepData;
            stepData.Start();
        }
    }

    /// <summary>
    /// 自动执行步骤
    /// </summary>
    public void AutoExecuteStep(int stepId = 1)
    {
        int allStepNum = CfgManager.GetInstance().AllStepNum;
        stepId = stepId < 1 || stepId > allStepNum ? 1 : stepId;
        StartStep(stepId);
    }

    //协程延迟执行下一个步骤
    private IEnumerator IE_ExcuteNextStep(StepData nextStepData)
    {
        Debug.Log($"延迟几秒执行： {nextStepData.CfgStepData.Delay}");
        yield return new WaitForSeconds(nextStepData.CfgStepData.Delay);
        StartStep(nextStepData.CfgStepData.StepId);

    }

    //步骤完成监听
    private void OnFinishStepCallBack(params object[] objs)
    {
        if (!Game.GetInstance().IsAutoExecute)
        {
            LogUtilits.LogErrorFormat($"是否是步骤自动执行模式： {Game.GetInstance().IsAutoExecute}");
            return;
        }
        if (objs.ArrayIsNull())
        {
            LogUtilits.LogErrorFormat("空");
            return;
        }
        StepData stepData = objs[0] as StepData;
        if (null == m_CurStepData)
        {
            LogUtilits.LogErrorFormat("当前执行的步骤为空，请检查！！！");
            return;
        }
        if (m_CurStepData.CfgStepData.StepId != stepData.CfgStepData.StepId)
        {
            LogUtilits.LogErrorFormat($"步骤id不相同 curId: {m_CurStepData.CfgStepData.StepId}  stepId: {stepData.CfgStepData.StepId}");
            return;
        }
        int nextStepId = m_CurStepData.CfgStepData.StepId;
        if (nextStepId < CfgManager.GetInstance().AllStepNum)
        {
            nextStepId++;
        }
        else
        {
            return;
        }
        //下一个步骤数据
        StepData nextStepData = CfgManager.GetInstance().GetStepData(nextStepId);
        m_CorAutoNext = StartCoroutine(IE_ExcuteNextStep(nextStepData));
    }




}
