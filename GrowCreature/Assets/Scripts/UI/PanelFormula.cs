using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PanelFormula : MonoBehaviour
    {
        public Transform content;
        public FormulaItem formulaItemPrefab;
        public Button btnCross;

        public FormulaItemInitialization selectedFormulaItem;

        [Foldout("Item BG Sprite")] [SerializeField]
        private Sprite _spriteItemSelected;

        [Foldout("Item BG Sprite")] [SerializeField]
        private Sprite _spriteItemDeSelected;

        private List<FormulaItem> _formulaItemList = new List<FormulaItem>();

        private void Start()
        {
            btnCross.onClick.AddListener(() => { gameObject.SetActive(false); });

            PopulateItem();
        }

        private void PopulateItem()
        {
            FormulaItem currentSelectedItem = null;
            for (int i = 0; i < Formula.FormulaList.Count; i++)
            {
                FormulaItem formulaItem = Instantiate(formulaItemPrefab, content);
                formulaItem.AssignItem(Formula.FormulaList[i], this);
                if (Formula.FormulaList[i].Split(',')[0] == SaveManager.GetInstance().GetSaveFormula())
                {
                    currentSelectedItem = formulaItem;
                }

                _formulaItemList.Add(formulaItem);
            }

            SelectItemBG(currentSelectedItem);
        }

        public void SelectItemBG(FormulaItem formulaItem = null)
        {
            for (int i = 0; i < _formulaItemList.Count; i++)
            {
                _formulaItemList[i].formulaItemBg.sprite = _spriteItemDeSelected;
            }

            if (formulaItem != null) formulaItem.formulaItemBg.sprite = _spriteItemSelected;
        }
    }
}