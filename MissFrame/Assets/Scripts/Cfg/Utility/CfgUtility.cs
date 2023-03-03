using MissFrame;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CfgUtility : Singleton<CfgUtility>
{

    private Tables cfgTab;
    public Tables CfgTab
    {
        get
        {
            if (null == cfgTab)
            {
                cfgTab = new Tables(TabLoader);
            }
            return cfgTab;
        }
    }
    private JSONNode TabLoader(string fileName)
    {
        string path = Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json";
        string jsonData = File.ReadAllText(path);
        return JSON.Parse(jsonData);
    }





}
