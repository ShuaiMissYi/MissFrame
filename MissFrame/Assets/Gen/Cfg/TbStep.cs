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

public sealed partial class TbStep
{
    private readonly Dictionary<int, Cfg.CfgStepData> _dataMap;
    private readonly List<Cfg.CfgStepData> _dataList;
    
    public TbStep(JSONNode _json)
    {
        _dataMap = new Dictionary<int, Cfg.CfgStepData>();
        _dataList = new List<Cfg.CfgStepData>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = Cfg.CfgStepData.DeserializeCfgStepData(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.StepId, _v);
        }
        PostInit();
    }

    public Dictionary<int, Cfg.CfgStepData> DataMap => _dataMap;
    public List<Cfg.CfgStepData> DataList => _dataList;

    public Cfg.CfgStepData GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Cfg.CfgStepData Get(int key) => _dataMap[key];
    public Cfg.CfgStepData this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}