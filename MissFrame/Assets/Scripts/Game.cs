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

    //�Զ�ִ�в���
    public void AutoExecuteStep(int stepId=0)
    {
        if (IsAutoExecute)
        {
            StepController.GetInstance().AutoExecuteStep(stepId);
        }
    }


}
