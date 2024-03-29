using DG.Tweening;
using NaughtyAttributes;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrowFetus
{
    public class GrowFetusUI : MonoBehaviour
    {
        public Button btnFormula;
        public Button btnIsland;
        public GameObject panelFormula;
        public Transform formulaBgTr;

        [HorizontalLine] public FormulaItemInitialization _selectedFormulaItem;

        private void Start()
        {
            _selectedFormulaItem.AssignItem(SaveManager.GetInstance().GetSaveFormula());
            btnFormula.onClick.AddListener(OpenFormulaPanel);
            btnIsland.onClick.AddListener(() => { SceneManager.LoadScene("Island"); });
        }

        private void OpenFormulaPanel()
        {
            formulaBgTr.transform.localScale = Vector3.one * .75f;
            panelFormula.SetActive(true);
            formulaBgTr.transform.DOScale(Vector3.one, .25f);
        }
    }
}