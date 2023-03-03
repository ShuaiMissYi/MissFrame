using MissFrame.Cfg;
using UnityEngine;

public class StepData
{
    //������������
    private CfgStepData m_CfgStepData;

    public CfgStepData CfgStepData { get => m_CfgStepData; }

    public StepData(int stepId)
    {
        InitCfgData(stepId);
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
    }


}
