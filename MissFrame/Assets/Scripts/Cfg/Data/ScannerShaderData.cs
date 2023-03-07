using MissFrame.Cfg;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerShaderData : ExpressionBase
{

    private CfgScannerShaderData m_CfgScannerShaderData;
    public CfgScannerShaderData CfgScannerShaderData => m_CfgScannerShaderData;

    public ScannerShaderData(int id) : base(id)
    {
    }

    public override void InitCfgData()
    {
        base.InitCfgData();
        m_CfgScannerShaderData = CfgUtility.GetInstance().CfgTab.TbScannerShader.Get(m_ShowId);
        if (null == m_CfgScannerShaderData)
        {
            Debug.LogErrorFormat($"该id：{m_ShowId}对应的配置为空！！！");
            return;
        }
    }

    public override void Run(SubStepData data)
    {
        base.Run(data);
        EventDispatcher.GetInstance().DispatchEvent(StepShowType.ScannerShader, this);
    }

    public override void Reset()
    {
        base.Reset();
        ResetStep(EventType.StopTweenLookAt);
    }



}
