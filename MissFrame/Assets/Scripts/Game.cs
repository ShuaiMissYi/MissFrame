using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private void Awake()
    {
        InitSingleton();
        DontDestroyOnLoad(gameObject);
    }


    private void InitSingleton()
    {
        //初始化配置表数据
        CfgManager.GetInstance().Init();
        //子步骤
        SubStepController.GetInstance().Init();
        //显隐
        ActiveController.GetInstance().Init();
        //特效路径
        EffectPathController.GetInstance().Init();
        //TweenMove
        TweenMoveController.GetInstance().Init();
        //TweenLookAt
        TweenLookAtController.GetInstance().Init();

    }

    public void ExecuteStep(int stepId)
    {
        StepController.GetInstance().IsAutoExcuteStep = false;
        StepController.GetInstance().StartStep(stepId);
    }

    public void ResetStep()
    {
        StepController.GetInstance().ResetCurRunningStep();
    }

    //自动执行步骤
    public void AutoExecuteStep(int stepId=0)
    {
        StepController.GetInstance().IsAutoExcuteStep = true;
        StepController.GetInstance().AutoExecuteStep(stepId);
    }


}
