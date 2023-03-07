using MissFrame.Cfg;
using MissFrame.Trigger;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CfgManager : Singleton<CfgManager>
{


    //����-��������
    private Dictionary<int,StepData> DicStepData = new Dictionary<int, StepData>();
    //�Ӳ���-��������
    private Dictionary<int, SubStepData> DicSubStepData = new Dictionary<int, SubStepData>();
    //����-��������
    private Dictionary<int, ActiveData> DicActiveData = new Dictionary<int, ActiveData>();
    //��Ч�켣-��������
    private Dictionary<int, EffectData> DicEffectShowPathData = new Dictionary<int, EffectData>();
    //Tween�ƶ�-��������
    private Dictionary<int, TweenMoveData> DicTweenMoveData = new Dictionary<int, TweenMoveData>();
    //TweenLookAt-��������
    private Dictionary<int, TweenLookAtData> DicTweenLookAtData = new Dictionary<int, TweenLookAtData>();
    //ScannerShader-��������
    private Dictionary<int, ScannerShaderData> DicScannerShaderData = new Dictionary<int, ScannerShaderData>();
    //Highlight-��������
    private Dictionary<int, HighlightData> DicHighlightData = new Dictionary<int, HighlightData>();


    /// <summary>
    /// �ܲ�����
    /// </summary>
    public int AllStepNum => DicStepData.Count;


    public override void Init()
    {
        InitAllCfgData();
    }

    #region ��ʼ�����õ�������
    private void InitAllCfgData()
    {
        InitSubStepCfg();
        InitStepCfg();
        InitActiveCfg();
        InitEffectShowPathCfg();
        InitTweenMoveCfg();
        InitTweenLookAtCfg();
        InitScannerShaderCfg();
        InitHighlightCfg();
        InitCfgOther();
        Debug.Log("��ʼ��  InitAllCfgData");
    }

    //��ʼ���������
    private void InitCfgOther()
    {
        //��������-�Ӳ���֮��Ĺ���
        foreach (var item in DicStepData.Values)
        {
            item.InitSubStepDataRelevance();
        }
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
            if (DicSubStepData.ContainsKey(key))
            {
                LogUtilits.LogErrorFormat($"������ͬ��key �� {key}");
                return;
            }
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

    //��ʼ����Tween�ƶ�����������
    private void InitTweenMoveCfg()
    {
        Dictionary<int, CfgTweenMoveData> map = CfgUtility.GetInstance().CfgTab.TbTweenMove.DataMap;
        foreach (var key in map.Keys)
        {
            TweenMoveData data = new TweenMoveData(key);
            DicTweenMoveData.Add(key, data);
        }
    }
    //��ʼ����Tween�ƶ�����������
    private void InitTweenLookAtCfg()
    {
        Dictionary<int, CfgTweenLookAtData> map = CfgUtility.GetInstance().CfgTab.TbTweenLookAt.DataMap;
        foreach (var key in map.Keys)
        {
            TweenLookAtData data = new TweenLookAtData(key);
            DicTweenLookAtData.Add(key, data);
        }
    }
    //��ʼ����ScannerShader����������
    private void InitScannerShaderCfg()
    {
        Dictionary<int, CfgScannerShaderData> map = CfgUtility.GetInstance().CfgTab.TbScannerShader.DataMap;
        foreach (var key in map.Keys)
        {
            ScannerShaderData data = new ScannerShaderData(key);
            DicScannerShaderData.Add(key, data);
        }
    }
    //��ʼ����HighlightData����������
    private void InitHighlightCfg()
    {
        Dictionary<int, CfgHighlightData> map = CfgUtility.GetInstance().CfgTab.TbHighlight.DataMap;
        foreach (var key in map.Keys)
        {
            HighlightData data = new HighlightData(key);
            DicHighlightData.Add(key, data);
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
    /// <summary>
    /// ��ȡ������-TweenMove
    /// </summary>
    /// <param name="id"></param>
    public TweenMoveData GetTweenMoveData(int id)
    {
        TweenMoveData data = null;
        if (!DicTweenMoveData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ��TweenMove������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-TweenLookAtData
    /// </summary>
    /// <param name="id"></param>
    public TweenLookAtData GetTweenLookAtData(int id)
    {
        TweenLookAtData data = null;
        if (!DicTweenLookAtData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ��TweenLookAtData������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-ScannerShader
    /// </summary>
    /// <param name="id"></param>
    public ScannerShaderData GetScannerShaderData(int id)
    {
        ScannerShaderData data = null;
        if (!DicScannerShaderData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ��ScannerShaderData������Ϊ�գ�����");
        }
        return data;
    }
    /// <summary>
    /// ��ȡ������-HighlightData
    /// </summary>
    /// <param name="id"></param>
    public HighlightData GetHighlightData(int id)
    {
        HighlightData data = null;
        if (!DicHighlightData.TryGetValue(id, out data))
        {
            Debug.LogError($"��id��{id} ��Ӧ��HighlightData������Ϊ�գ�����");
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
            case StepShowType.TweenMove:
                data = GetTweenMoveData(id);
                break;
            case StepShowType.TweenLookAt:
                data = GetTweenLookAtData(id);
                break;
            case StepShowType.ScannerShader:
                data = GetScannerShaderData(id);
                break;
            case StepShowType.Highlight:
                data = GetHighlightData(id);
                break;
        }
        return data;
    }

    #endregion


}
