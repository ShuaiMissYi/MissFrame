using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : SingletonMono<HighlightController>
{
    private HighlightData m_HighlightData;

    public override void Init()
    {
        base.Init();
        AddListener();

    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.Highlight, OnHighlightCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopHightHighlight, OnStopHighlightCallBack);
    }

    private void OnHighlightCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull(true))
        {
            return;
        }
        m_HighlightData = objs[0] as HighlightData;
        ExecuteHighlight(m_HighlightData);
    }

    private void ExecuteHighlight(HighlightData data)
    {
        if (null == data)
        {
            return;
        }
        GameObject root = GameObject.Find(data.CfgHighlightData.RootTargetName);
        Transform target = root.transform.Find(data.CfgHighlightData.RelativeTargetPath);
        if (null == target)
        {
            LogUtilits.LogErrorFormat($"对象空");
            return;
        }
        HighlightableObject script = target.GetComponent<HighlightableObject>();
        if (null == script)
        {
            LogUtilits.LogErrorFormat("脚本为空");
            return;
        }
        if (data.CfgHighlightData.IsExecute)
        {
            script.FlashingOn();
        }
        else
        {
            script.FlashingOff();
        }
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_HighlightData.SubStep);
    }

    private void OnStopHighlightCallBack(params object[] objs)
    {

    }








}
