//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace MissFrame.Cfg
{ 

public sealed partial class CfgSubStepData :  Bright.Config.BeanBase 
{
    public CfgSubStepData(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["desc"].IsString) { throw new SerializationException(); }  Desc = _json["desc"]; }
        { var __json0 = _json["prepositionStepIdList"]; if(!__json0.IsArray) { throw new SerializationException(); } PrepositionStepIdList = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  PrepositionStepIdList.Add(__v0); }   }
        { var __json0 = _json["nextStepIdList"]; if(!__json0.IsArray) { throw new SerializationException(); } NextStepIdList = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  NextStepIdList.Add(__v0); }   }
        { if(!_json["triggerType"].IsNumber) { throw new SerializationException(); }  TriggerType = (Trigger.StepShowType)_json["triggerType"].AsInt; }
        { if(!_json["triggerId"].IsNumber) { throw new SerializationException(); }  TriggerId = _json["triggerId"]; }
        { if(!_json["isNeedReset"].IsBoolean) { throw new SerializationException(); }  IsNeedReset = _json["isNeedReset"]; }
        PostInit();
    }

    public CfgSubStepData(int id, string desc, System.Collections.Generic.List<int> prepositionStepIdList, System.Collections.Generic.List<int> nextStepIdList, Trigger.StepShowType triggerType, int triggerId, bool isNeedReset ) 
    {
        this.Id = id;
        this.Desc = desc;
        this.PrepositionStepIdList = prepositionStepIdList;
        this.NextStepIdList = nextStepIdList;
        this.TriggerType = triggerType;
        this.TriggerId = triggerId;
        this.IsNeedReset = isNeedReset;
        PostInit();
    }

    public static CfgSubStepData DeserializeCfgSubStepData(JSONNode _json)
    {
        return new Cfg.CfgSubStepData(_json);
    }

    /// <summary>
    /// 子步骤id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 子步骤描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 前置步骤id列表
    /// </summary>
    public System.Collections.Generic.List<int> PrepositionStepIdList { get; private set; }
    /// <summary>
    /// 后续步骤id列表
    /// </summary>
    public System.Collections.Generic.List<int> NextStepIdList { get; private set; }
    /// <summary>
    /// 子步骤表现类型
    /// </summary>
    public Trigger.StepShowType TriggerType { get; private set; }
    /// <summary>
    /// 根据&lt;triggerType&gt;类型去对应的表中读取<br/>表现id配置
    /// </summary>
    public int TriggerId { get; private set; }
    /// <summary>
    /// 执行下一个步骤时，是否需要重置到步骤执行之前的状态
    /// </summary>
    public bool IsNeedReset { get; private set; }

    public const int __ID__ = -1047162116;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Desc:" + Desc + ","
        + "PrepositionStepIdList:" + Bright.Common.StringUtil.CollectionToString(PrepositionStepIdList) + ","
        + "NextStepIdList:" + Bright.Common.StringUtil.CollectionToString(NextStepIdList) + ","
        + "TriggerType:" + TriggerType + ","
        + "TriggerId:" + TriggerId + ","
        + "IsNeedReset:" + IsNeedReset + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
