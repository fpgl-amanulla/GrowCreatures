using System.Collections.Generic;
using Core;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PanelFormula : MonoBehaviour
    {
        [ShowAssetPreview(32, 32)] public GameObject matchEmPrefab;
        public Transform content;
        public FormulaItem formulaItemPrefab;
        public Button btnNewFormula;
        public Button btnGrow;
        public Button btnCross;

        public FormulaItemInitialization selectedFormulaItem;

        [Foldout("Item BG Sprite")] [SerializeField]
        private Sprite _spriteItemSelected;

        [Foldout("Item BG Sprite")] [SerializeField]
        private Sprite _spriteItemDeSelected;

        private List<FormulaItem> _formulaItemList = new List<FormulaItem>();

        private void Start()
        {
            ActionManager.Instance.OnFormulaAddition += PopulateItem;
        }

        private void OnDestroy()
        {
            if (ActionManager.Instance) ActionManager.Instance.OnFormulaAddition -= PopulateItem;
        }

        private void OnEnable()
        {
            btnCross.onClick.AddListener(() => { gameObject.SetActive(false); });
            btnGrow.onClick.AddListener(() => { gameObject.SetActive(false); });
            btnNewFormula.onClick.AddListener(CreateNewFormulaCallBack);

            PopulateItem();
        }

        private void PopulateItem(string formula = null)
        {
            if (_formulaItemList.Count > 0)
            {
                for (int i = 0; i < _formulaItemList.Count; i++)
                {
                    Destroy(_formulaItemList[i].gameObject);
                }
            }

            _formulaItemList.Clear();

            FormulaItem currentSelectedItem = null;
            List<string> myFormulaList = Formula.GetMyFormulaList();
            for (int i = 0; i < myFormulaList.Count; i++)
            {
                FormulaItem formulaItem = Instantiate(formulaItemPrefab, content);
                formulaItem.AssignItem(myFormulaList[i], this);
                if (myFormulaList[i] == SaveManager.GetInstance().GetSaveFormula())
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

        private void CreateNewFormulaCallBack()
        {
            GameObject matchEm = Instantiate(matchEmPrefab, this.transform.root);
            //this.gameObject.SetActive(false);
        }
    }
}