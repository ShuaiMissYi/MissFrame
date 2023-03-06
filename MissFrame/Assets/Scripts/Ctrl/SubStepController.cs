using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStepController : SingletonMono<SubStepController>
{
    public override void Init()
    {
        base.Init();
        AddListener();
    }


    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist( EventType.FinishSubStep, OnFinishSubStepCallBack);
    }

    //完成子步骤监听
    private void OnFinishSubStepCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull())
        {
            return;
        }
        SubStepData data = objs[0] as SubStepData;
        if (data.IsNull())
        {
            return;
        }
        data.FinishStep();
        data.Run();
    }



}
