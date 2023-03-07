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

public sealed partial class TbHighlight
{
    private readonly Dictionary<int, Cfg.CfgHighlightData> _dataMap;
    private readonly List<Cfg.CfgHighlightData> _dataList;
    
    public TbHighlight(JSONNode _json)
    {
        _dataMap = new Dictionary<int, Cfg.CfgHighlightData>();
        _dataList = new List<Cfg.CfgHighlightData>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = Cfg.CfgHighlightData.DeserializeCfgHighlightData(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Cfg.CfgHighlightData> DataMap => _dataMap;
    public List<Cfg.CfgHighlightData> DataList => _dataList;

    public Cfg.CfgHighlightData GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Cfg.CfgHighlightData Get(int key) => _dataMap[key];
    public Cfg.CfgHighlightData this[int key] => _dataMap[key];

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