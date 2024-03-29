using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Merge;
using NaughtyAttributes;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrowFetus
{
    public class LiquidContainerController : MonoBehaviour
    {
        private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");
        public FormulaChecker formulaChecker;
        public Transform baseContainer;
        public LiquidDummy liquidDummyPrefab;

        [Foldout("UI Components")] public GameObject txtCellCreated;
        [Foldout("UI Components")] public Sprite growCellActive;
        [Foldout("UI Components")] public Sprite growCellDeActive;
        [Foldout("UI Components")] public Button btnGrowCell;
        [Foldout("UI Components")] public GameObject formulaButtonHolder;

        [HorizontalLine(color: EColor.Blue)] [Space(20)]
        public ParticleSystem cfx_magical_source;

        public ParticleSystem cfx_magical_Aura_source;


        [HorizontalLine(color: EColor.Blue)] public ParticleSystem leftPourEffect;
        public ParticleSystem rightPourEffect;

        [HorizontalLine(color: EColor.Blue)] public GameObject fetus;

        [HorizontalLine(color: EColor.Blue)] [Expandable]
        public MergeObjectSetListSO mergeObjectSetListSO;


        [HorizontalLine] [SerializeField] private GameObject jarLimit;


        private List<LiquidDummy> allLiquidDummy = new List<LiquidDummy>();
        private LiquidDummy _liquidDummy;
        private Renderer _liquidRenderer;
        private float fillAmount = 1.25f;
        private bool startPouring = false;
        [HideInInspector] public bool isJarFilled = false;

        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        public UnityAction OnPourComplete;
        private static readonly int Colour = Shader.PropertyToID("_Colour");

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
            if (isJarFilled) return;
            //Update value
            fillAmount -= .01f;
            _liquidRenderer.material.SetFloat(FillAmount, fillAmount);
            if (!(fillAmount <= 0)) return;

            //Debug.Log("Filled");
            isJarFilled = true;
            OnPourComplete?.Invoke();
            bool isCorrect = FormulaChecker.Instance.CheckFormula();
            //Debug.Log(isCorrect);
            if (!isCorrect)
            {
                StartCoroutine(ResetContainer());

                return;
            }

            jarLimit.SetActive(false);
            //Select MergeObjectSO
            string productId = SaveManager.GetInstance().GetSaveFormulaProductId();
            AppDelegate.GetInstance().SelectedMergeObjectSo = mergeObjectSetListSO.GetMergeObjectSetListSo(productId);

            cfx_magical_source.gameObject.SetActive(true);
            cfx_magical_Aura_source.gameObject.SetActive(true);
            StartCoroutine(GenerateFetus());
        }

        private IEnumerator GenerateFetus()
        {
            formulaButtonHolder.SetActive(false);
            yield return new WaitForSeconds(3.0f);

            MergeObjectSetSO MergeObjectSo = AppDelegate.GetInstance().SelectedMergeObjectSo;
            GameObject mergeObjIns = MergeObjectSo.GetMergeObjPrefab(0, false);
            mergeObjIns.transform.SetParent(fetus.transform);
            mergeObjIns.transform.localPosition = Vector3.zero;
            fetus.SetActive(true);

            cfx_magical_source.gameObject.SetActive(false);
            DeactivateAllLiquidDummy();
            txtCellCreated.SetActive(true);
            ActiveGrowCellBtn(true);
            btnGrowCell.gameObject.SetActive(true);
        }

        private void DeactivateAllLiquidDummy()
        {
            for (int i = 0; i < allLiquidDummy.Count; i++) allLiquidDummy[i].gameObject.SetActive(false);
        }

        private IEnumerator ResetContainer()
        {
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < allLiquidDummy.Count; i++)
            {
                Destroy(allLiquidDummy[i].gameObject);
            }

            allLiquidDummy.Clear();

            formulaChecker.ResetChecker();
            fillAmount = 1.25f;
            isJarFilled = false;
        }

        public void StartPourLiquid(ConicalFlask conicalFlask)
        {
            if (_liquidDummy != null)
                if (_liquidDummy.LiquidType == conicalFlask.liquidType)
                {
                    StartCoroutine(ActivatePourEffect(conicalFlask, true));
                    return;
                }

            Renderer previousRenderer = null;
            if (_liquidRenderer != null) previousRenderer = _liquidRenderer;

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
            StartCoroutine(ActivatePourEffect(conicalFlask, true));
        }

        private IEnumerator ActivatePourEffect(ConicalFlask _conicalFlask, bool status)
        {
            ParticleSystem pourLiquidEffect = _conicalFlask.pouringPointType == PouringPointType.Left
                ? leftPourEffect
                : rightPourEffect;

            //Set Color
            ParticleSystem.MainModule main = pourLiquidEffect.main;
            main.startColor = _conicalFlask.liquidRenderer.material.GetColor(Colour);
            switch (status)
            {
                case true:
                    pourLiquidEffect.Play();
                    break;
                default:
                    pourLiquidEffect.Stop();
                    yield break;
            }

            yield return new WaitForSeconds(.25f);
            startPouring = true;
        }

        public void StopPourLiquid(ConicalFlask conicalFlask)
        {
            startPouring = false;
            StartCoroutine(ActivatePourEffect(conicalFlask, false));
        }
    }
}