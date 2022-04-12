using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FormulaItem : MonoBehaviour
    {
        public Image imgFinalProduct;
        public Button btnFormula;
        public Image formulaItemBg;

        private FormulaItemInitialization _formulaItemInitialization;
        private string _formula;
        private PanelFormula _panelFormula;

        private void Awake()
        {
            btnFormula.onClick.AddListener(FormulaCallBack);
            _formulaItemInitialization = GetComponent<FormulaItemInitialization>();
        }

        private void FormulaCallBack()
        {
            //Select Formula
            _panelFormula.SelectItemBG(this);
            _panelFormula.selectedFormulaItem.AssignItem(_formula);
            SaveManager.GetInstance().SaveFormula(_formula);
        }

        public void AssignItem(string formula, PanelFormula panelFormula)
        {
            _formula = formula;
            _panelFormula = panelFormula;
            _formulaItemInitialization.AssignItem(formula);
        }
    }
}