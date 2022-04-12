using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FormulaChecker : FormulaItemInitialization
    {
        public static FormulaChecker Instance;

        [SerializeField] private string formula;

        private int updateCount = 0;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            for (int i = 0; i < formulaComponentList.Count; i++) formulaComponentList[i].gameObject.SetActive(false);
        }

        public void UpdateFormula(int value)
        {
            Debug.Log(value);

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
            string selectedFormula = SaveManager.GetInstance().GetSaveFormula();
            formula = formula.Remove(formula.Length - 1);
            return selectedFormula == formula;
        }
    }
}