using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StepController : SingletonMono<StepController>
{
    //当前步骤数据
    private StepData m_CurStepData;

    private Coroutine m_CorAutoNext;

    //是否自动执行步骤
    public bool IsAutoExcuteStep;


    private void Awake()
    {
        EventDispatcher.GetInstance().Regist(EventType.FinishStep, OnFinishStepCallBack);
    }


    public void StartStep(int stepId)
    {
        if (null!= m_CurStepData)
        {
            m_CurStepData.Reset();
            m_CurStepData = null;
        }
        StepData stepData = CfgManager.GetInstance().GetStepData(stepId);
        if (null != stepData)
        {
            m_CurStepData = stepData;
            stepData.Start();
        }
    }

    //重置步骤
    public void ResetStep()
    {
        if (GameUtilits.GameIsNull(m_CurStepData,false))
        {
            Debug.Log("当前无操作步骤");
            return;
        }
        m_CurStepData.Reset();
        m_CurStepData = null;
    }


    /// <summary>
    /// 自动执行步骤
    /// </summary>
    public void AutoExecuteStep(int stepId = 1)
    {
        int allStepNum = CfgManager.GetInstance().AllStepNum;
        stepId = stepId < 1 || stepId > allStepNum ? 1 : stepId;
        ResetStep();
        StepData stepData = CfgManager.GetInstance() .GetStepData(stepId);
        m_CurStepData = stepData;
        stepData.Start();
    }

    //协程延迟执行下一个步骤
    private IEnumerator IE_ExcuteNextStep(StepData curStepData,StepData nextStepData)
    {
        Debug.Log($"延迟几秒执行： {nextStepData.CfgStepData.Delay}");
        yield return new WaitForSeconds(nextStepData.CfgStepData.Delay);
        curStepData.Reset();
        m_CurStepData = nextStepData;
        nextStepData.Start();
    }

    //步骤完成监听
    private void OnFinishStepCallBack(params object[] objs)
    {
        if (!IsAutoExcuteStep)
        {
            Debug.LogErrorFormat($"IsAutoExcuteStep： {IsAutoExcuteStep}");
            return;
        }
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            Debug.LogErrorFormat("空");
            return;
        }
        StepData stepData = objs[0] as StepData;
        if (null == m_CurStepData)
        {
            Debug.LogError("当前执行的步骤为空，请检查！！！");
            return;
        }
        if (m_CurStepData.CfgStepData.StepId != stepData.CfgStepData.StepId)
        {
            Debug.LogErrorFormat($"步骤id不相同 curId: {m_CurStepData.CfgStepData.StepId}  stepId: {stepData.CfgStepData.StepId}");
            return;
        }
        int nextStepId = m_CurStepData.CfgStepData.StepId;
        if (nextStepId < CfgManager.GetInstance().AllStepNum)
        {
            nextStepId++;
        }
        else
        {
            nextStepId = 1;
        }
        //下一个步骤数据
        StepData nextStepData = CfgManager.GetInstance().GetStepData(nextStepId);
        m_CorAutoNext = StartCoroutine(IE_ExcuteNextStep(m_CurStepData,nextStepData));
    }




}
