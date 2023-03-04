using MissFrame.Cfg;
using UnityEngine;

public class StepData
{
    //������������
    private CfgStepData m_CfgStepData;

    public CfgStepData CfgStepData { get => m_CfgStepData; }

    //�Ӳ�������
    public int AllSubStepNun => CfgStepData.ChildStepIdList.Count;
    //��ǰ����ɵ��Ӳ�����
    private int m_CurFinishSubStepNum = 0;


    //�����Ƿ����
    private bool m_IsFinish = false;
    public bool IsFinish=>m_IsFinish;
   

    public StepData(int stepId)
    {
        InitCfgData(stepId);
        EventDispatcher.GetInstance().Regist(EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //��ʼ����������
    public void InitCfgData(int stepId)
    {
        //��������
        m_CfgStepData = CfgUtility.GetInstance().CfgTab.TbStep.Get(stepId);
        if (null == m_CfgStepData)
        {
            Debug.LogErrorFormat($"�ò���id��{stepId}��Ӧ�Ĳ�������Ϊ�գ�����");
            return;
        }
    }
    //�����Ӳ������
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
        //1.��ʼִ��
        //Debug.LogFormat($"{m_CfgStepData.StepId} ���Ϳ�ʼִ�м���");
        //EventDispatcher.GetInstance().DispatchEvent( EventType.StartExecuteStep,this);
        Debug.Log("��ʼִ��");
        RunStep();
    }

    public void Reset()
    {
        //1.ֹͣ��������߼�
        //Debug.LogFormat($"{m_CfgStepData.StepId}����ִֹͣ�м���");
        Debug.Log("ִֹͣ��");
        ResetStep();
    }

    public void Dispos()
    {
        EventDispatcher.GetInstance().UnRegist(EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //ִ���Ӳ�������
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

    //�����Ӳ�������
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
            //�Ӳ���ȫ��ִ�����
            m_IsFinish = true;
            EventDispatcher.GetInstance().DispatchEvent(EventType.FinishStep,this);
        }
    }


}
