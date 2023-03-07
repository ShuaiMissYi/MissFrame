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


    //步骤-配置数据
    private Dictionary<int,StepData> DicStepData = new Dictionary<int, StepData>();
    //子步骤-配置数据
    private Dictionary<int, SubStepData> DicSubStepData = new Dictionary<int, SubStepData>();
    //显隐-配置数据
    private Dictionary<int, ActiveData> DicActiveData = new Dictionary<int, ActiveData>();
    //特效轨迹-配置数据
    private Dictionary<int, EffectData> DicEffectShowPathData = new Dictionary<int, EffectData>();
    //Tween移动-配置数据
    private Dictionary<int, TweenMoveData> DicTweenMoveData = new Dictionary<int, TweenMoveData>();
    //TweenLookAt-配置数据
    private Dictionary<int, TweenLookAtData> DicTweenLookAtData = new Dictionary<int, TweenLookAtData>();
    //ScannerShader-配置数据
    private Dictionary<int, ScannerShaderData> DicScannerShaderData = new Dictionary<int, ScannerShaderData>();
    //Highlight-配置数据
    private Dictionary<int, HighlightData> DicHighlightData = new Dictionary<int, HighlightData>();


    /// <summary>
    /// 总步骤数
    /// </summary>
    public int AllStepNum => DicStepData.Count;


    public override void Init()
    {
        InitAllCfgData();
    }

    #region 初始化配置的数据类
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
        Debug.Log("初始化  InitAllCfgData");
    }

    //初始化配置相关
    private void InitCfgOther()
    {
        //建立步骤-子步骤之间的关联
        foreach (var item in DicStepData.Values)
        {
            item.InitSubStepDataRelevance();
        }
    }

    //初始化《步骤》流程配置数据
    private void InitStepCfg()
    {
        //步骤数据类
        Dictionary<int, CfgStepData> dataMap = CfgUtility.GetInstance().CfgTab.TbStep.DataMap;
        foreach (var key in dataMap.Keys)
        {
            StepData data = new StepData(key);
            DicStepData.Add(key, data);
        }
    }
    //初始化《子步骤》流程配置数据
    private void InitSubStepCfg()
    {
        //子步骤数据类
        Dictionary<int, CfgSubStepData> subStepMap = CfgUtility.GetInstance().CfgTab.TbSubStep.DataMap;
        foreach (var key in subStepMap.Keys)
        {
            SubStepData data = new SubStepData(key);
            if (DicSubStepData.ContainsKey(key))
            {
                LogUtilits.LogErrorFormat($"存在相同的key ： {key}");
                return;
            }
            DicSubStepData.Add(key, data);
        }
    }
    //初始化《显隐》流程配置数据
    private void InitActiveCfg()
    {
        //子步骤数据类
        Dictionary<int, CfgActiveData> subStepMap = CfgUtility.GetInstance().CfgTab.TbActive.DataMap;
        foreach (var key in subStepMap.Keys)
        {
            ActiveData data = new ActiveData(key);
            DicActiveData.Add(key, data);
        }
    }
    //初始化《特效轨迹》配置数据
    private void InitEffectShowPathCfg()
    {
        Dictionary<int, CfgShowEffectPathData> map = CfgUtility.GetInstance().CfgTab.TbShowEffectPath.DataMap;
        foreach (var key in map.Keys)
        {
            EffectData data = new EffectData(key);
            DicEffectShowPathData.Add(key, data);
        }
    }

    //初始化《Tween移动》配置数据
    private void InitTweenMoveCfg()
    {
        Dictionary<int, CfgTweenMoveData> map = CfgUtility.GetInstance().CfgTab.TbTweenMove.DataMap;
        foreach (var key in map.Keys)
        {
            TweenMoveData data = new TweenMoveData(key);
            DicTweenMoveData.Add(key, data);
        }
    }
    //初始化《Tween移动》配置数据
    private void InitTweenLookAtCfg()
    {
        Dictionary<int, CfgTweenLookAtData> map = CfgUtility.GetInstance().CfgTab.TbTweenLookAt.DataMap;
        foreach (var key in map.Keys)
        {
            TweenLookAtData data = new TweenLookAtData(key);
            DicTweenLookAtData.Add(key, data);
        }
    }
    //初始化《ScannerShader》配置数据
    private void InitScannerShaderCfg()
    {
        Dictionary<int, CfgScannerShaderData> map = CfgUtility.GetInstance().CfgTab.TbScannerShader.DataMap;
        foreach (var key in map.Keys)
        {
            ScannerShaderData data = new ScannerShaderData(key);
            DicScannerShaderData.Add(key, data);
        }
    }
    //初始化《HighlightData》配置数据
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


    #region 数据获取
    /// <summary>
    /// 获取数据类-步骤
    /// </summary>
    /// <param name="id"></param>
    public StepData GetStepData(int id)
    {
        StepData data = null;
        if (!DicStepData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的步骤数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-子步骤
    /// </summary>
    /// <param name="id"></param>
    public SubStepData GetSubStepData(int id)
    {
        SubStepData data = null;
        if (!DicSubStepData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的子步骤数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-显隐类
    /// </summary>
    /// <param name="id"></param>
    public ActiveData GetActiveData(int id)
    {
        ActiveData data = null;
        if (!DicActiveData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的显隐类数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-特效轨迹
    /// </summary>
    /// <param name="id"></param>
    public EffectData GetEffectShowPathData(int id)
    {
        EffectData data = null;
        if (!DicEffectShowPathData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的特效轨迹类数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-TweenMove
    /// </summary>
    /// <param name="id"></param>
    public TweenMoveData GetTweenMoveData(int id)
    {
        TweenMoveData data = null;
        if (!DicTweenMoveData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的TweenMove类数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-TweenLookAtData
    /// </summary>
    /// <param name="id"></param>
    public TweenLookAtData GetTweenLookAtData(int id)
    {
        TweenLookAtData data = null;
        if (!DicTweenLookAtData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的TweenLookAtData类数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-ScannerShader
    /// </summary>
    /// <param name="id"></param>
    public ScannerShaderData GetScannerShaderData(int id)
    {
        ScannerShaderData data = null;
        if (!DicScannerShaderData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的ScannerShaderData类数据为空，请检查");
        }
        return data;
    }
    /// <summary>
    /// 获取数据类-HighlightData
    /// </summary>
    /// <param name="id"></param>
    public HighlightData GetHighlightData(int id)
    {
        HighlightData data = null;
        if (!DicHighlightData.TryGetValue(id, out data))
        {
            Debug.LogError($"该id：{id} 对应的HighlightData类数据为空，请检查");
        }
        return data;
    }


    //获取表现类数据
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
