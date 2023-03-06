using MissFrame.Cfg;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubStepData
{
    //�Ӳ�����������
    private CfgSubStepData m_CfgSubStepData;
    public CfgSubStepData CfgSubStepData => m_CfgSubStepData;

    private StepData m_StepData;
    public StepData MainStepData => m_StepData;

    //�Ӳ����Ƿ����
    private bool m_IsFinish = false;
    public bool IsFinish => m_IsFinish;

    //�Ӳ����Ƿ�ʼ
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
            Debug.LogErrorFormat($"���Ӳ���id��{id}��Ӧ���Ӳ�������Ϊ�գ�����");
            return;
        }
    }

    //����������
    public void RelevanceStep(StepData stepData)
    {
        m_StepData = stepData;
    }

    //�����ڲ��ж������Ƿ��������
    public void Run()
    {
        //�ж��Ƿ���ǰ�ò���
        bool isHave = IsHavePreposition();
        if (isHave)
        {
            //ǰ�ò����Ƿ����
            bool isFinish = PrepositionIsFinish();
            if (!isFinish)
            {
                return;
            }
        }
        //�ж������Ƿ������
        if (IsFinish)
        {
            //ִ���Ӳ���
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
    /// �Ƿ���ǰ�ò���
    /// </summary>
    /// <returns></returns>
    public bool IsHavePreposition()
    {
        return m_CfgSubStepData.PrepositionStepIdList.Count > 0;
    }
    /// <summary>
    /// ǰ�ò����Ƿ����
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
    /// ��ȡǰ�ò���id�б�
    /// </summary>
    /// <returns></returns>
    public List<int> GetPrepositionIdList()
    {
        return m_CfgSubStepData.PrepositionStepIdList;
    }

    /// <summary>
    /// �������
    /// </summary>
    public void FinishStep()
    {
        m_IsFinish = true;
        Debug.LogFormat($"�������   {m_CfgSubStepData.Desc}");
    }
    

    //�����Ӳ���
    public void ResetSubStep()
    {
        //�����Ӳ�����󣺱��֡���״̬
        if (IsRuning)
        {
            //����������У�������
            ExpressionBase data = CfgManager.GetInstance().GetExpressInfo(m_CfgSubStepData.TriggerType, m_CfgSubStepData.TriggerId);
            if (null != data)
            {
                data.Reset();
            }
        }
        ResetStepState();
    }
    /// <summary>
    /// ���ò���״̬ 
    /// </summary>
    private void ResetStepState()
    {
        m_IsFinish = false;
        m_IsRuning = false;
    }







}
