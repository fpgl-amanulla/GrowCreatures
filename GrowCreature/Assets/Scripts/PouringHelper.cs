using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public static class PouringHelper
{
    private static Sequence _sequenceGoToPour;

    public static void GoToPour(ConicalFlask conicalFlask, Transform pouringPoint, Quaternion quaternion,
        UnityAction onPouringPointReached = null)
    {
        _sequenceGoToPour = DOTween.Sequence();
        //conicalFlask.liquidRenderer.material.SetFloat(LiquidContainerController.FillAmount, .5f);
        _sequenceGoToPour.Append(conicalFlask.conicalFlaskTr.DOLocalMove(pouringPoint.position, 1.0f))
            .OnComplete(delegate
            {
                onPouringPointReached?.Invoke();
            });
        conicalFlask.conicalFlaskTr.DOLocalRotateQuaternion(quaternion, 1.0f);
    }

    public static void GoBackOwnPosition(ConicalFlask conicalFlask)
    {
        _sequenceGoToPour.Kill();
        //conicalFlask.liquidRenderer.material.SetFloat(LiquidContainerController.FillAmount, 1f);
        conicalFlask.conicalFlaskTr.DOLocalMove(conicalFlask.initialPos, 1.0f);
        conicalFlask.conicalFlaskTr.DOLocalRotateQuaternion(Quaternion.identity, 1.0f);
    }
}