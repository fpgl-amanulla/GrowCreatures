using System;
using UnityEngine;

namespace UI
{
    public class PanelFormula : MonoBehaviour
    {
        public Transform content;
        public FormulaItem formulaItemPrefab;

        private void Start()
        {
            PopulateItem();
        }

        private void PopulateItem()
        {
            for (int i = 0; i < Formula.FormulaList.Count; i++)
            {
                FormulaItem formulaItem = Instantiate(formulaItemPrefab, content);
                formulaItem.AssignItem(Formula.FormulaList[i]);
            }
        }
    }
}