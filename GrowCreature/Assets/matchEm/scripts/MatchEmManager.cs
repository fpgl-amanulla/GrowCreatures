using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using DG.Tweening;
using Singleton;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace matchEm.scripts
{
    public class MatchEmManager : Singleton<MatchEmManager>
    {
        public Button btnCross;
        public List<GameObject> LevelPrefabs = new List<GameObject>();
        public int levelNo;
        public GameObject levelParent;
        public UIParticle victoryEffect;
        [SerializeField] private GameObject newFormula;
        [SerializeField] private Button btnCollect;


        public override void Start()
        {
            btnCross.onClick.AddListener(CrossCallBack);
            btnCollect.onClick.AddListener(CrossCallBack);
            LoadLvlPrefab();
        }

        private void LoadLvlPrefab()
        {
            levelNo = PlayerPrefs.GetInt("current_Level_matchEm", 0);
            GameObject lvlPrefab = Instantiate(LevelPrefabs[levelNo], levelParent.transform.position,
                Quaternion.identity, levelParent.transform);
            SlotsManager.Instance.itemsHolder = lvlPrefab;
        }

        public void LevelComplete()
        {
            if (levelNo + 1 >= LevelPrefabs.Count)
            {
                PlayerPrefs.SetInt("current_Level_matchEm", 0);
            }
            else
            {
                PlayerPrefs.SetInt("current_Level_matchEm", levelNo + 1);
            }

            levelNo++;

            StartCoroutine(MatchComplete(1.0f));
        }

        private IEnumerator MatchComplete(float delayTime)
        {
            yield return new WaitForSeconds(.25f);
            victoryEffect.Play();
            SlotsManager.Instance.gameObject.SetActive(false);
            yield return new WaitForSeconds(delayTime);
            //Create Formula and add to DB
            SaveManager.GetInstance().AddNewFormula(Formula.GetNextFormula());
            newFormula.SetActive(true);
            newFormula.GetComponent<RectTransform>().localScale = Vector3.one * .5f;
            newFormula.GetComponent<RectTransform>().DOScale(Vector3.one, .5f);
        }

        private void CrossCallBack()
        {
            Destroy(this.gameObject);
        }
    }
}