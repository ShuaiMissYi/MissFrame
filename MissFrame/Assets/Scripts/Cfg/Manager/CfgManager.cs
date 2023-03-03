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



    #region 初始化配置的数据类
    private void InitAllCfgData()
    {
        Debug.Log("初始化  InitAllCfgData");
        InitStepCfg();
        InitSubStepCfg();
        InitActiveCfg();
        InitEffectShowPathCfg();
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
        }
        return data;
    }
    #endregion



}
