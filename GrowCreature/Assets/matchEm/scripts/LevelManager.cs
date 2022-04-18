using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine.SceneManagement;
using Singleton;
using UnityEngine;

namespace matchEm.scripts
{
    [System.Serializable]
    public class LevelManager : Singleton<LevelManager>
    {
        public List<GameObject> LevelPrefabs = new List<GameObject>();
        public int levelNo;
        public GameObject levelParent;
        public UIParticle victoryEffect;


        public override void Start()
        {
            LoadLvlPrefab();
        }

        public void LoadLvlPrefab()
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

        public IEnumerator MatchComplete(float delayTime)
        {
            yield return new WaitForSeconds(.25f);
            victoryEffect.Play();
            yield return new WaitForSeconds(delayTime);
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}