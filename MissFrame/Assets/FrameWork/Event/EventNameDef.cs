/// <summary>
/// �¼�����ö��
/// </summary>
public enum EventType{ 
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

