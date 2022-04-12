using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace GrowFetus
{
    public static class PouringHelper
    {
        private static Sequence _sequenceGoToPour;

        private static bool _onPouringPointReached = false;
        private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

        public static void GoToPour(ConicalFlask conicalFlask, Transform pouringPoint, Quaternion quaternion,
            UnityAction onPouringPointReached = null)
        {
            _sequenceGoToPour = DOTween.Sequence();
            conicalFlask.liquidRenderer.material.SetFloat(FillAmount, .5f);
            _sequenceGoToPour.Append(conicalFlask.conicalFlaskTr.DOLocalMove(pouringPoint.position, 1.0f))
                .OnComplete(delegate
                {
                    _onPouringPointReached = true;
                    FormulaChecker.Instance.UpdateFormula((int)conicalFlask.liquidType);
                    onPouringPointReached?.Invoke();
                });
            conicalFlask.conicalFlaskTr.DOLocalRotateQuaternion(quaternion, 1.0f);
        }

        public static void GoBackOwnPosition(ConicalFlask conicalFlask)
        {
            _sequenceGoToPour.Kill();
            if (_onPouringPointReached)
            {
                conicalFlask.liquidRenderer.material.SetFloat(FillAmount, 1f);
            }

            conicalFlask.conicalFlaskTr.DOLocalMove(conicalFlask.initialPos, 1.0f);
            conicalFlask.conicalFlaskTr.DOLocalRotateQuaternion(Quaternion.identity, 1.0f);
        }
    }
}