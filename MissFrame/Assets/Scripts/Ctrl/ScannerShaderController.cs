using MissFrame.Trigger;
using Scanner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerShaderController : SingletonMono<ScannerShaderController>
{
    private ScannerShaderData m_ScannerShaderData;

    public override void Init()
    {
        base.Init();
        AddListener();

    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.ScannerShader, OnScannerShaderCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopScannerShader, OnStopScannerShaderCallBack);
    }

    private void OnScannerShaderCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull(true))
        {
            return;
        }
        m_ScannerShaderData = objs[0] as ScannerShaderData;
        ExecuteScanner(m_ScannerShaderData);
    }

    private void ExecuteScanner(ScannerShaderData data)
    {
        if (null == data)
        {
            return;
        }
        GameObject root = GameObject.Find(data.CfgScannerShaderData.RootTargetName);
        Transform target = root.transform.Find(data.CfgScannerShaderData.RelativeTargetPath);
        if (null == target) 
        {
            LogUtilits.LogErrorFormat($"对象空");
            return;
        }
        Demo2 scannerScript = target.GetComponent<Demo2>();
        if (null == scannerScript) 
        {
            LogUtilits.LogErrorFormat("Demo2  脚本为空");
            return;
        }
        scannerScript.m_IsExecute = true;
    }

    private void OnStopScannerShaderCallBack(params object[] objs)
    {

    }




}
