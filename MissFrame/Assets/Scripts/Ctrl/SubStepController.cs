using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStepController : SingletonMono<SubStepController>
{

    private void Awake()
    {
        AddListener();
    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist( EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //����Ӳ������
    private void OnFinishSubStepCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs)||objs.Length == 0)
        {
            return;
        }
        SubStepData data = objs[0] as SubStepData;
        if (GameUtilits.GameIsNull(data))
        {
            return;
        }
        data.FinishStep();
        data.Run();
    }



}
