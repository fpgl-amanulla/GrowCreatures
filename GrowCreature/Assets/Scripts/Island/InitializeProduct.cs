using System;
using System.Collections.Generic;
using Core;
using Merge;
using UnityEngine;

namespace Island
{
    public class InitializeProduct : MonoBehaviour
    {
        [SerializeField] private MergeObjectSetListSO _mergeObjectSetListSo;
        [SerializeField] private Transform animalHolder;
        private GameData _gameData;

        private void Start()
        {
            _gameData = CustomSave.LoadData<GameData>(SaveManager.myProductFileName);
            List<MyProductData> myProductDataList = _gameData.myProductDataList;
            Debug.Log(myProductDataList.Count);
            for (int i = 0; i < myProductDataList.Count; i++)
            {
                MergeObjectSetSO mergeObjectSetListSo =
                    _mergeObjectSetListSo.GetMergeObjectSetListSo(myProductDataList[i].id);
                GameObject productGameObject =
                    mergeObjectSetListSo.GetMergeObjPrefab(mergeObjectSetListSo.AllMergeObjInfo.Count - 1);

                productGameObject.transform.SetParent(animalHolder);
                productGameObject.transform.localScale = Vector3.one;
                productGameObject.transform.localPosition =
                    SaveManager.productPositionList[myProductDataList[i].positionIndex];
            }
        }
    }
}