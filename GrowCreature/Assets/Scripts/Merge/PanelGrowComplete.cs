using Core;
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

        [ContextMenu("CollectCallBack")]
        private void CollectCallBack()
        {
            btnCollect.gameObject.SetActive(false);

            string productId = AppDelegate.GetInstance().SelectedMergeObjectSo.productId;

            GameData gameData = CustomSave.LoadData<GameData>(SaveManager.myProductFileName) ??
                                GameData.CreateInstance();
            gameData.myProductDataList.Add(new MyProductData(productId, gameData.myProductDataList.Count));
            CustomSave.SaveData(gameData, SaveManager.myProductFileName);

            SceneManager.LoadScene("Island");
        }
    }
}