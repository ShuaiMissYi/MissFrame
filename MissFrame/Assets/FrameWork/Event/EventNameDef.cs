/// <summary>
/// 事件监听枚举
/// </summary>
public enum EventType{ 
    None = 0,
    //开始执行步骤
    StartExecuteStep = 1,
    //完成子步骤监听
    FinishSubStep,
    //完成步骤监听
    FinishStep,
    //停止-显隐
    StopExecuteActive,
    //停止-特效路径
    StopExecuteEffectPath,
    //停止-Tween移动
    StopTweenMove,
    //停止-Tween移动
    StopTweenLookAt,
}

