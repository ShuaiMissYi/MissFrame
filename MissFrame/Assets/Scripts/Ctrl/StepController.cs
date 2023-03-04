using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StepController : SingletonMono<StepController>
{
    //��ǰ��������
    private StepData m_CurStepData;

    private Coroutine m_CorAutoNext;

    //�Ƿ��Զ�ִ�в���
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

    //���ò���
    public void ResetStep()
    {
        if (GameUtilits.GameIsNull(m_CurStepData,false))
        {
            Debug.Log("��ǰ�޲�������");
            return;
        }
        m_CurStepData.Reset();
        m_CurStepData = null;
    }


    /// <summary>
    /// �Զ�ִ�в���
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

    //Э���ӳ�ִ����һ������
    private IEnumerator IE_ExcuteNextStep(StepData curStepData,StepData nextStepData)
    {
        Debug.Log($"�ӳټ���ִ�У� {nextStepData.CfgStepData.Delay}");
        yield return new WaitForSeconds(nextStepData.CfgStepData.Delay);
        curStepData.Reset();
        m_CurStepData = nextStepData;
        nextStepData.Start();
    }

    //������ɼ���
    private void OnFinishStepCallBack(params object[] objs)
    {
        if (!IsAutoExcuteStep)
        {
            Debug.LogErrorFormat($"IsAutoExcuteStep�� {IsAutoExcuteStep}");
            return;
        }
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            Debug.LogErrorFormat("��");
            return;
        }
        StepData stepData = objs[0] as StepData;
        if (null == m_CurStepData)
        {
            Debug.LogError("��ǰִ�еĲ���Ϊ�գ����飡����");
            return;
        }
        if (m_CurStepData.CfgStepData.StepId != stepData.CfgStepData.StepId)
        {
            Debug.LogErrorFormat($"����id����ͬ curId: {m_CurStepData.CfgStepData.StepId}  stepId: {stepData.CfgStepData.StepId}");
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
        //��һ����������
        StepData nextStepData = CfgManager.GetInstance().GetStepData(nextStepId);
        m_CorAutoNext = StartCoroutine(IE_ExcuteNextStep(m_CurStepData,nextStepData));
    }




}
