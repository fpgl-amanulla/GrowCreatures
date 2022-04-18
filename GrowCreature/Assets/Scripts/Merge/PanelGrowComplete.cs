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

            GameData gameData = JsonSave.LoadData<GameData>(SaveManager.gameDataFileName) ??
                                GameData.CreateInstance();
            gameData.myProductDataList.Add(new MyProductData(productId, gameData.myProductDataList.Count));
            JsonSave.SaveData(gameData, SaveManager.gameDataFileName);

            SceneManager.LoadScene("Island");
        }
    }
}