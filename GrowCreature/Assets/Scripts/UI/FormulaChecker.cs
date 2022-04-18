using System.Collections.Generic;
using DG.Tweening;
using GrowFetus;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FormulaChecker : MonoBehaviour
    {
        public static FormulaChecker Instance;
        public List<Image> formulaComponentList = new List<Image>();

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteCute;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteLove;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteStrength;

        [Foldout("Component Sprite")] [SerializeField]
        private Sprite _spriteSmart;
        [SerializeField] private string formula;
        public GameObject imgWrongFormula;

        private int updateCount = 0;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            for (int i = 0; i < formulaComponentList.Count; i++) formulaComponentList[i].gameObject.SetActive(false);
        }

        public void UpdateFormula(int value)
        {
            //Debug.Log(value);

            if (formula.Contains(value.ToString())) return;

            formula += value.ToString() + ',';
            formulaComponentList[updateCount].gameObject.SetActive(true);
            if (updateCount > 0)
                formulaComponentList[updateCount - 1].transform.GetChild(0).gameObject.SetActive(true);
            SetLiQuidSprite(value, updateCount);
            updateCount++;
        }

        public bool CheckFormula()
        {
            string selectedFormula = SaveManager.GetInstance().GetSaveFormulaOnly();
            formula = formula.Remove(formula.Length - 1);
            if (selectedFormula == formula) return true;
            imgWrongFormula.SetActive(true);
            imgWrongFormula.transform.DOScaleX(4, .25f);
            return false;
        }

        public void ResetChecker()
        {
            imgWrongFormula.SetActive(false);
            imgWrongFormula.transform.DOScaleX(0, .25f);
            for (int i = 0; i < formulaComponentList.Count; i++) formulaComponentList[i].gameObject.SetActive(false);
            formula = "";
            updateCount = 0;
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