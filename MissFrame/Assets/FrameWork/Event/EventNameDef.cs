/// <summary>
/// �¼�����ö��
/// </summary>
public enum EventType{ 
    None = 0,
    //��ʼִ�в���
    StartExecuteStep = 1,
    //����Ӳ������
    FinishSubStep,
    //��ɲ������
    FinishStep,
    //ֹͣ-����
    StopExecuteActive,
    //ֹͣ-��Ч·��
    StopExecuteEffectPath,
    //ֹͣ-Tween�ƶ�
    StopTweenMove,
    //ֹͣ-Tween�ƶ�
    StopTweenLookAt,
}

