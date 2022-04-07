using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrowFetus
{
    public class LiquidContainerController : MonoBehaviour
    {
        public Transform baseContainer;
        public LiquidDummy liquidDummyPrefab;

        [HorizontalLine(color: EColor.Blue)] [Header("UI Components")]
        public GameObject txtCellCreated;

        public Sprite growCellActive;
        public Sprite growCellDeActive;
        public Button btnGrowCell;
        public GameObject formulaButtonHolder;

        private bool startPouring = false;
        private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

        [HorizontalLine(color: EColor.Blue)] [Space(20)]
        public ParticleSystem cfx_magical_source;

        public GameObject fetus;

        private List<LiquidDummy> allLiquidDummy = new List<LiquidDummy>();
        private LiquidDummy _liquidDummy;
        private Renderer _liquidRenderer;
        private float fillAmount = 1.25f;

        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        public UnityAction OnPourComplete;

        private void Start()
        {
            ActiveGrowCellBtn(false);
            btnGrowCell.onClick.AddListener(GrowCellCallBack);

            Application.targetFrameRate = 30;
        }

        private void ActiveGrowCellBtn(bool status)
        {
            if (!status)
            {
                btnGrowCell.image.sprite = growCellDeActive;
                btnGrowCell.interactable = false;
            }
            else
            {
                btnGrowCell.image.sprite = growCellActive;
                btnGrowCell.interactable = true;
            }
        }

        private void GrowCellCallBack()
        {
            SceneManager.LoadScene("Merge");
        }

        private void Update()
        {
            if (!startPouring) return;
            if (fillAmount < 0) return;
            //Update value
            fillAmount -= .01f;
            _liquidRenderer.material.SetFloat(FillAmount, fillAmount);
            if (!(fillAmount <= 0)) return;

            Debug.Log("Filled");
            OnPourComplete?.Invoke();
            cfx_magical_source.gameObject.SetActive(true);
            StartCoroutine(GenerateFetus());
        }

        private IEnumerator GenerateFetus()
        {
            formulaButtonHolder.SetActive(false);
            yield return new WaitForSeconds(3.0f);
            fetus.SetActive(true);
            cfx_magical_source.gameObject.SetActive(false);
            DeactivateAllLiquidDummy();
            txtCellCreated.SetActive(true);
            ActiveGrowCellBtn(true);
            btnGrowCell.gameObject.SetActive(true);
        }

        private void DeactivateAllLiquidDummy()
        {
            for (int i = 0; i < allLiquidDummy.Count; i++)
            {
                allLiquidDummy[i].gameObject.SetActive(false);
            }
        }

        public void StartPourLiquid(bool status, ConicalFlask conicalFlask)
        {
            if (_liquidDummy != null)
                if (_liquidDummy.LiquidType == conicalFlask.liquidType)
                {
                    startPouring = true;
                    return;
                }

            Renderer previousRenderer = null;
            if (_liquidRenderer != null)
            {
                previousRenderer = _liquidRenderer;
            }

            _liquidDummy = Instantiate(liquidDummyPrefab, baseContainer);
            allLiquidDummy.Add(_liquidDummy);
            _liquidDummy.LiquidType = conicalFlask.liquidType;
            _liquidRenderer = _liquidDummy.GetComponent<Renderer>();
            _liquidRenderer.material = conicalFlask.liquidRenderer.material;
            int index = allLiquidDummy.Count - 3;
            if (index >= 0)
            {
                allLiquidDummy[index].GetComponent<Renderer>().material
                    .SetColor(TopColor, _liquidRenderer.material.GetColor(TopColor));
            }

            _liquidRenderer.material.SetFloat(FillAmount, fillAmount);
            if (previousRenderer != null)
                previousRenderer.material.SetColor(TopColor, _liquidRenderer.material.GetColor(TopColor));

            startPouring = true;
        }

        public void StopPourLiquid()
        {
            startPouring = false;
        }
    }
}