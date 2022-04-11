using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Merge
{
    public class PanelGrowComplete : MonoBehaviour
    {
        [SerializeField] private Button btnCollect;

        private void Start()
        {
            btnCollect.onClick.AddListener(CollectCallBack);
        }

        private void CollectCallBack()
        {
            btnCollect.gameObject.SetActive(false);
            SceneManager.LoadScene("Island");
        }
    }
}