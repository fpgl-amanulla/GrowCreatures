using DG.Tweening;
using Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace matchEm.scripts
{
    public class UIManager : Singleton<UIManager>
    {
        public Button btnPlay;
        public GameObject slotsHolder;

        private new void Awake()
        {
            slotsHolder.SetActive(false);
            btnPlay.image.rectTransform.DOScale(Vector3.one * .9f, .8f).SetLoops(-1, LoopType.Yoyo);

            btnPlay.onClick.AddListener(() =>
            {
                slotsHolder.SetActive(true);
                btnPlay.gameObject.SetActive(false);
                LevelManager.Instance.LoadLvlPrefab();
            });
        }
    }
}