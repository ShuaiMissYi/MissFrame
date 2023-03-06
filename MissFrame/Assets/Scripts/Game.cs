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
        //��ʼ�����ñ�����
        CfgManager.GetInstance().Init();
        //�Ӳ���
        SubStepController.GetInstance().Init();
        //����
        ActiveController.GetInstance().Init();
        //��Ч·��
        EffectPathController.GetInstance().Init();
        //TweenMove
        TweenMoveController.GetInstance().Init();

    }

    public void ExecuteStep(int stepId)
    {
        StepController.GetInstance().IsAutoExcuteStep = false;
        StepController.GetInstance().StartStep(stepId);
    }

    public void ResetStep()
    {
        StepController.GetInstance().ResetStep();
    }

    //�Զ�ִ�в���
    public void AutoExecuteStep(int stepId=0)
    {
        StepController.GetInstance().IsAutoExcuteStep = true;
        StepController.GetInstance().AutoExecuteStep(stepId);
    }


}
