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

    private void Start()
    {
       
    }


    private void InitSingleton()
    {
        //子步骤
        SubStepController.GetInstance().Init();
        //显隐
        ActiveController.GetInstance().Init();
        //特效路径
        EffectPathController.GetInstance().Init();
    }

    public void ExecuteStep(int stepId)
    {
        StepController.GetInstance().StartStep(stepId);
    }

    public void ResetStep()
    {
        StepController.GetInstance().ResetStep();
    }


}
