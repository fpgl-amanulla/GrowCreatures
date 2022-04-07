using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Merge
{
    public class MergeController : MonoBehaviour
    {
        public static MergeController Instance;

        public bool assignEditorSO = false;

        [HorizontalLine()] [FormerlySerializedAs("fetusSetSo")]
        public MergeObjectSetSO mergeObjectSetSo;

        public Button btnGenerate;
        public GameObject panelGrowComplete;
        [FormerlySerializedAs("fetusHolder")] public Transform ObjectHolder;
        public GameObject mergeEffectFX;
        public GameObject popUpTextPrefab;

        private int currentMergeObjState = 0;
        private const int maxMergeObjState = 4;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            btnGenerate.onClick.AddListener(GenerateFetus);
        }

        private void Start()
        {
            if (Application.isEditor)
            {
                if (assignEditorSO == false)
                    mergeObjectSetSo = AppDelegate.GetInstance().selectedMergeObjectSo;
            }
            else
                mergeObjectSetSo = AppDelegate.GetInstance().selectedMergeObjectSo;

            mergeObjectSetSo.GetMergeObjPrefab(0);
        }

        private void GenerateFetus()
        {
            GameObject mergeObjPrefab = mergeObjectSetSo.GetMergeObjPrefab(0);
            InitFetus(mergeObjPrefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4, 4), 0));
        }

        private void InitFetus(GameObject mergeObj, Vector3 position)
        {
            mergeObj.transform.SetParent(ObjectHolder);
            mergeObj.transform.position = position;
        }

        public bool IsMergePossible(MergeObject mergeObject1, MergeObject mergeObject2) =>
            mergeObject1.MergeObjectState == mergeObject2.MergeObjectState;

        public void MergeObject(MergeObject mergeObject1)
        {
            //Play Merge Effect
            Instantiate(mergeEffectFX, mergeObject1.transform.position, Quaternion.identity);

            //Merge & instantiate next object
            GameObject mergeObj = mergeObjectSetSo.GetNextMergeObjPrefab(mergeObject1.MergeObjectState);
            MergeObject mergeObject = mergeObj.GetComponent<MergeObject>();
            if (mergeObject.MergeObjectState > currentMergeObjState)
            {
                ShowMergePopUpText(mergeObj.transform);
                currentMergeObjState = mergeObject.MergeObjectState;
            }

            InitFetus(mergeObj, mergeObject1.transform.position);

            //Check for final state
            if (mergeObject.MergeObjectState < maxMergeObjState) return;
            btnGenerate.gameObject.SetActive(false);

            StartCoroutine(ShowAllState());
            //Complete
        }

        private IEnumerator ShowAllState()
        {
            yield return new WaitForSeconds(2.0f);
            ObjectHolder.gameObject.SetActive(false);
            for (int i = 0; i < mergeObjectSetSo.AllMergeObjInfo.Count; i++)
            {
                GameObject fetus = mergeObjectSetSo.GetMergeObjPrefab(i, false);
                //fetus.GetComponent<MergeObject>().dragEnabled = false;
                fetus.transform.position = Vector3.zero;

                if (i == maxMergeObjState)
                {
                    panelGrowComplete.SetActive(true);
                    fetus.transform.rotation = Quaternion.Euler(0, 150, 0);
                    yield break;
                }

                yield return new WaitForSeconds(1.0f);
                Destroy(fetus);
            }
        }

        private readonly List<string> popUpStringList = new List<string>()
        {
            "Nice", "Amazing", "Awesome", "Good", "Fabulous"
        };

        private void ShowMergePopUpText(Component parent)
        {
            GameObject popUpTextObj = Instantiate(popUpTextPrefab, parent.transform);
            TextMeshPro popUpText = popUpTextObj.GetComponent<TextMeshPro>();
            StartCoroutine(TypeString(popUpStringList[Random.Range(0, popUpStringList.Count)], popUpText));
            popUpText.DOFade(0, 0f);
            popUpText.DOFade(1, 1.0f).OnComplete(delegate { Destroy(popUpTextObj); });
        }

        private IEnumerator TypeString(string stringToType, TMP_Text popUpText)
        {
            string strPopUp = "";
            foreach (char item in stringToType)
            {
                strPopUp += item;
                popUpText.text = strPopUp;
                yield return new WaitForSeconds(.05f);
            }
        }

        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}