using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StepController : SingletonMono<StepController>
{
    //��ǰ��������
    private StepData m_CurStepData;

    private Coroutine m_CorAutoNext;


    private void Awake()
    {
        EventDispatcher.GetInstance().Regist(EventType.FinishStep, OnFinishStepCallBack);
    }

    //���в���
    public void StartStep(int stepId)
    {
        if (null!= m_CurStepData)
        {
            //ִ�иò���ʱ���ж���һ�������Ƿ���Ҫ����
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
    /// �Զ�ִ�в���
    /// </summary>
    public void AutoExecuteStep(int stepId = 1)
    {
        int allStepNum = CfgManager.GetInstance().AllStepNum;
        stepId = stepId < 1 || stepId > allStepNum ? 1 : stepId;
        StartStep(stepId);
    }

    //Э���ӳ�ִ����һ������
    private IEnumerator IE_ExcuteNextStep(StepData nextStepData)
    {
        Debug.Log($"�ӳټ���ִ�У� {nextStepData.CfgStepData.Delay}");
        yield return new WaitForSeconds(nextStepData.CfgStepData.Delay);
        StartStep(nextStepData.CfgStepData.StepId);

    }

    //������ɼ���
    private void OnFinishStepCallBack(params object[] objs)
    {
        if (!Game.GetInstance().IsAutoExecute)
        {
            LogUtilits.LogErrorFormat($"�Ƿ��ǲ����Զ�ִ��ģʽ�� {Game.GetInstance().IsAutoExecute}");
            return;
        }
        if (objs.ArrayIsNull())
        {
            LogUtilits.LogErrorFormat("��");
            return;
        }
        StepData stepData = objs[0] as StepData;
        if (null == m_CurStepData)
        {
            LogUtilits.LogErrorFormat("��ǰִ�еĲ���Ϊ�գ����飡����");
            return;
        }
        if (m_CurStepData.CfgStepData.StepId != stepData.CfgStepData.StepId)
        {
            LogUtilits.LogErrorFormat($"����id����ͬ curId: {m_CurStepData.CfgStepData.StepId}  stepId: {stepData.CfgStepData.StepId}");
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
        //��һ����������
        StepData nextStepData = CfgManager.GetInstance().GetStepData(nextStepId);
        m_CorAutoNext = StartCoroutine(IE_ExcuteNextStep(nextStepData));
    }




}
