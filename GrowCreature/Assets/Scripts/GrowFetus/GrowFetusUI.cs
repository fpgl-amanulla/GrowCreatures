using System;
using DG.Tweening;
using NaughtyAttributes;
using UI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace GrowFetus
{
    public class GrowFetusUI : MonoBehaviour
    {
        public Button btnFormula;
        public GameObject panelFormula;
        public Transform formulaBgTr;

        [HorizontalLine] public FormulaItemInitialization _selectedFormulaItem;

        private void Start()
        {
            _selectedFormulaItem.AssignItem(SaveManager.GetInstance().GetSaveFormula());
            btnFormula.onClick.AddListener(OpenFormulaPanel);
        }

        private void OpenFormulaPanel()
        {
            formulaBgTr.transform.localScale = Vector3.one * .75f;
            panelFormula.SetActive(true);
            formulaBgTr.transform.DOScale(Vector3.one, .25f);
        }
    }
}