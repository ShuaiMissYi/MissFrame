using UnityEngine;

public class Game : SingletonMono<Game>
{

    public bool IsAutoExecute = false;


    private void Awake()
    {
        InitSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AutoExecuteStep();
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
        //ScannerShader
        ScannerShaderController.GetInstance().Init();
        //Hightlight
        HighlightController.GetInstance().Init();

    }

    public void ExecuteStep(int stepId)
    {
        StepController.GetInstance().StartStep(stepId);
    }

    //自动执行步骤
    public void AutoExecuteStep(int stepId=0)
    {
        if (IsAutoExecute)
        {
            StepController.GetInstance().AutoExecuteStep(stepId);
        }
    }


}
