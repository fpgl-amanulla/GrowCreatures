using System.Collections.Generic;
using System.Linq;
using Core;
using GrowFetus;
using Merge;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FormulaItemInitialization : MonoBehaviour
    {
        public MergeObjectSetListSO mergeObjectSetListSo;
        public Image imgFinalProduct;
        public List<Image> formulaComponentList = new List<Image>();

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteCute;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteLove;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteStrength;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteSmart;

        private void Start()
        {
            ActionManager.Instance.OnFormulaAddition += AssignItem;
        }

        private void OnDisable()
        {
            if (ActionManager.Instance) ActionManager.Instance.OnFormulaAddition -= AssignItem;
        }

        public void AssignItem(string formula)
        {
            List<string> componentList = formula.Split(';')[0].Split(',').ToList();
            string productId = formula.Split(';')[1];
            if (imgFinalProduct)
                imgFinalProduct.sprite = mergeObjectSetListSo.GetMergeObjectSetListSo(productId).productIcon;

            for (int i = 0; i < formulaComponentList.Count; i++)
            {
                formulaComponentList[i].gameObject.SetActive(false);
                formulaComponentList[i].transform.GetChild(0).gameObject.SetActive(true);
            }

            for (int i = 0; i < componentList.Count; i++)
            {
                formulaComponentList[i].gameObject.SetActive(true);
                if (i == componentList.Count - 1)
                    formulaComponentList[i].transform.GetChild(0).gameObject.SetActive(false);

                int liquidType = int.Parse(componentList[i]);
                SetLiQuidSprite(liquidType, i);
            }
        }

        private void SetLiQuidSprite(int liquidType, int i)
        {
            switch (liquidType)
            {
                case (int) LiquidType.Cute:
                    formulaComponentList[i].sprite = _spriteCute;
                    break;
                case (int) LiquidType.Love:
                    formulaComponentList[i].sprite = _spriteLove;
                    break;
                case (int) LiquidType.Strength:
                    formulaComponentList[i].sprite = _spriteStrength;
                    break;
                case (int) LiquidType.Smart:
                    formulaComponentList[i].sprite = _spriteSmart;
                    break;
                default:
                    break;
            }
        }
    }
}