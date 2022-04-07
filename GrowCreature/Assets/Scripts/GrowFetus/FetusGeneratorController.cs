using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrowFetus
{
    public class FetusGeneratorController : MonoBehaviour
    {
        private ButtonInputHandler _buttonInputHandler;

        public LiquidContainerController liquidContainerController;
        public List<ConicalFlask> allConicalFlask = new List<ConicalFlask>();

        [Header("Pouring Points")] [SerializeField]
        private Transform pouringPointLeft;

        [SerializeField] private Transform pouringPointRight;

        private ConicalFlask _conicalFlask;

        private void Start()
        {
            _buttonInputHandler = ButtonInputHandler.Instance;
            _buttonInputHandler.OnButtonPointerChanges += OnButtonPointerChanges;
            liquidContainerController.OnPourComplete += ResetConicalFlaskPos;
        }

        private void OnDestroy()
        {
            _buttonInputHandler.OnButtonPointerChanges -= OnButtonPointerChanges;
            liquidContainerController.OnPourComplete -= ResetConicalFlaskPos;
        }

        private void OnButtonPointerChanges(ButtonLiquidHandler buttonLiquidHandler, bool isPointerDown)
        {
            _conicalFlask = GetConicalFlask(buttonLiquidHandler);
            if (isPointerDown)
            {
                Transform pouringPoint;
                Quaternion flaskEndQuaternion = Quaternion.Euler(0, 0, -90);
                if (_conicalFlask.pouringPointType == PouringPointType.Left)
                    pouringPoint = pouringPointLeft;
                else
                {
                    pouringPoint = pouringPointRight;
                    flaskEndQuaternion = Quaternion.Euler(0, 0, 90);
                }

                PouringHelper.GoToPour(_conicalFlask, pouringPoint, flaskEndQuaternion, OnPouringPointReached);
            }
            else
            {
                PouringHelper.GoBackOwnPosition(_conicalFlask);
                //Stop Pouring
                liquidContainerController.StopPourLiquid(_conicalFlask);
            }
        }

        private ConicalFlask GetConicalFlask(ButtonLiquidHandler liquidHandler)
        {
            return (from t in allConicalFlask where t.liquidType == liquidHandler.liquidType select t)
                .FirstOrDefault();
        }

        private void ResetConicalFlaskPos()
        {
            PouringHelper.GoBackOwnPosition(_conicalFlask);
            liquidContainerController.StopPourLiquid(_conicalFlask);
        }

        private void OnPouringPointReached()
        {
            //Pour Liquid
            liquidContainerController.StartPourLiquid(_conicalFlask);
        }
    }
}