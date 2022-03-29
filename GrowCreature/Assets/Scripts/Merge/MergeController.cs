using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GrowFetus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Merge
{
    public class MergeController : MonoBehaviour
    {
        public static MergeController Instance;

        public FetusSetSO fetusSetSo;
        public Button btnGenerate;
        public GameObject panelGrowComplete;
        public Transform fetusHolder;
        public GameObject mergeEffectFX;
        public GameObject popUpTextPrefab;

        private int currentFetusState = 0;
        private const int maxFetusState = 4;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            btnGenerate.onClick.AddListener(GenerateFetus);
        }

        private void Start()
        {
            fetusSetSo.GetFetusPrefab(0);
        }

        private void GenerateFetus()
        {
            GameObject fetus = fetusSetSo.GetFetusPrefab(0);
            InitFetus(fetus, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4, 4), 0));
        }

        private void InitFetus(GameObject fetus, Vector3 position)
        {
            fetus.transform.SetParent(fetusHolder);
            fetus.transform.position = position;
        }

        public bool IsMergePossible(Fetus fetus_1, Fetus fetus_2) => fetus_1.FetusState == fetus_2.FetusState;

        public void MergeObject(Fetus fetus_1)
        {
            //Play Merge Effect
            Instantiate(mergeEffectFX, fetus_1.transform.position, Quaternion.identity);

            //Merge & instantiate next object
            GameObject fetusObj = fetusSetSo.GetNextFetusPrefab(fetus_1.FetusState);
            Fetus fetus = fetusObj.GetComponent<Fetus>();
            if (fetus.FetusState > currentFetusState)
            {
                ShowPopUpText(fetusObj.transform);
                currentFetusState = fetus.FetusState;
            }

            InitFetus(fetusObj, fetus_1.transform.position);

            //Check for final state
            if (fetus.FetusState < maxFetusState) return;
            btnGenerate.gameObject.SetActive(false);

            StartCoroutine(ShowAllState());
            //Complete
        }

        private IEnumerator ShowAllState()
        {
            yield return new WaitForSeconds(2.0f);
            fetusHolder.gameObject.SetActive(false);
            for (int i = 0; i < fetusSetSo.AllFetusInfo.Count; i++)
            {
                GameObject fetus = fetusSetSo.GetFetusPrefab(i);
                fetus.GetComponent<Fetus>().dragEnabled = false;
                fetus.transform.position = Vector3.zero;

                if (i == maxFetusState)
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

        private void ShowPopUpText(Component parent)
        {
            GameObject popUpTextObj = Instantiate(popUpTextPrefab, parent.transform);
            TextMeshPro popUpText = popUpTextObj.GetComponent<TextMeshPro>();
            popUpText.text = popUpStringList[Random.Range(0, popUpStringList.Count)];
            popUpText.DOFade(0, 0f);
            popUpText.DOFade(1, 1.0f).OnComplete(delegate { Destroy(popUpTextObj); });
        }
    }
}