using MissFrame.Cfg;
using MissFrame.Trigger;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CfgManager : Singleton<CfgManager>
{

    public CfgManager()
    {
        InitAllCfgData();
    }

    private Dictionary<int,StepData> DicStepData = new Dictionary<int, StepData>();
    private Dictionary<int, SubStepData> DicSubStepData = new Dictionary<int, SubStepData>();
    private Dictionary<int, ActiveData> DicActiveData = new Dictionary<int, ActiveData>();
    private Dictionary<int, EffectData> DicEffectShowPathData = new Dictionary<int, EffectData>();



    #region ��ʼ�����õ�������
    private void InitAllCfgData()
    {
        Debug.Log("��ʼ��  InitAllCfgData");
        InitStepCfg();
        InitSubStepCfg();
        InitActiveCfg();
        InitEffectShowPathCfg();
    }
    //��ʼ�������衷������������
    private void InitStepCfg()
    {
        //����������
        Dictionary<int, CfgStepData> dataMap = CfgUtility.GetInstance().CfgTab.TbStep.DataMap;
        foreach (var key in dataMap.Keys)
        {
            StepData data = new StepData(key);
            DicStepData.Add(key, data);
        }
    }
    //��ʼ�����Ӳ��衷������������
    private void InitSubStepCfg()
    {
        //�Ӳ���������
        Dictionary<int, CfgSubStepData> subStepMap = CfgUtility.GetInstance().CfgTab.TbSubStep.DataMap;
        foreach (var key in subStepMap.Keys)
        {
            SubStepData data = new SubStepData(key);
            DicSubStepData.Add(key, data);
        }
    }
    //��ʼ����������������������
    private void InitActiveCfg()
    {
        //�Ӳ���������
        Dictionary<int, CfgActiveData> subStepMap = CfgUtility.GetInstance().CfgTab.TbActive.DataMap;
        foreach (var key in subStepMap.Keys)
        {
            ActiveData data = new ActiveData(key);
            DicActiveData.Add(key, data);
        }
    }
    //��ʼ������Ч�켣����������
    private void InitEffectShowPathCfg()
    {
        Dictionary<int, CfgShowEffectPathData> map = CfgUtility.GetInstance().CfgTab.TbShowEffectPath.DataMap;
        foreach (var key in map.Keys)
        {
            EffectData data = new EffectData(key);
            DicEffectShowPathData.Add(key, data);
        }
    }

    #endregion


    #region ���ݻ�ȡ
    /// <summary>
    /// ��ȡ������-����
    /// </summary>
    /// <param name="id"></param>
    public StepData GetStepData(int id)
    {
        StepData data = null;
        if (!DicStepData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ�Ĳ�������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-�Ӳ���
    /// </summary>
    /// <param name="id"></param>
    public SubStepData GetSubStepData(int id)
    {
        SubStepData data = null;
        if (!DicSubStepData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ���Ӳ�������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-������
    /// </summary>
    /// <param name="id"></param>
    public ActiveData GetActiveData(int id)
    {
        ActiveData data = null;
        if (!DicActiveData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ������������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-��Ч�켣
    /// </summary>
    /// <param name="id"></param>
    public EffectData GetEffectShowPathData(int id)
    {
        EffectData data = null;
        if (!DicEffectShowPathData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ����Ч�켣������Ϊ�գ�����");
        }
        return data;
    }
    //��ȡ����������
    public ExpressionBase GetExpressInfo(StepShowType type,int id)
    {
        ExpressionBase data = null;
        switch (type)
        {
            case StepShowType.Active:
                data = GetActiveData(id);
                break;
            case StepShowType.ShowEffectPath:
                data = GetEffectShowPathData(id);
                break;
        }
        return data;
    }
    #endregion



}
