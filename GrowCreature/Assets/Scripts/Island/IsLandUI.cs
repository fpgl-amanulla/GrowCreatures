using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Island
{
    public class IsLandUI : MonoBehaviour
    {
        public static IsLandUI Instance = null;

        public Button btnGrow;
        public GameObject buttonHolder;

        private void Start()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            btnGrow.onClick.AddListener(() => { SceneManager.LoadScene("GrowFestus"); });
        }

        public void ButtonHolderSetActive(bool action) => buttonHolder.SetActive(action);
    }
}