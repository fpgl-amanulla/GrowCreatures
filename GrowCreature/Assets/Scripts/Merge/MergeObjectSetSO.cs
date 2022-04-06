using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Merge
{
    [Serializable]
    public class MergeObjectInfo
    {
        [FormerlySerializedAs("fetusState")] public int mergeObjState;
        [FormerlySerializedAs("fetusPrefab")] public GameObject mergeObjPrefab;
        public Texture2D mainTexture2D;
    }

    [CreateAssetMenu(fileName = "MergeObjectSet", menuName = "Create MergeObjectSet", order = 0)]
    public class MergeObjectSetSO : ScriptableObject
    {
        [FormerlySerializedAs("AllFetusInfo")] public List<MergeObjectInfo> AllMergeObjInfo = new List<MergeObjectInfo>();

        public GameObject GetNextMergeObjPrefab(int mergeObjState)
        {
            int stateIndex = mergeObjState + 1;
            GameObject mergeObjPrefab = AllMergeObjInfo[stateIndex].mergeObjPrefab;
            GameObject mergeObjIns = Instantiate(mergeObjPrefab);
            mergeObjIns.GetComponentInChildren<Renderer>().material.mainTexture = AllMergeObjInfo[stateIndex].mainTexture2D;
            MergeObject mergeObject = mergeObjIns.GetComponent<MergeObject>() == null
                ? mergeObjIns.AddComponent<MergeObject>()
                : mergeObjIns.GetComponent<MergeObject>();
            mergeObject.MergeObjectState = AllMergeObjInfo[stateIndex].mergeObjState;

            if (mergeObjIns.GetComponent<CameraBoundaries>() == null)
                mergeObjIns.AddComponent<CameraBoundaries>();

            if (mergeObjIns.GetComponent<Rigidbody>() == null) mergeObjIns.AddComponent<Rigidbody>();
            Rigidbody rigidbody = mergeObjIns.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.drag = 1000;

            return mergeObjIns.gameObject;
        }

        public GameObject GetMergeObjPrefab(int mergeObjState)
        {
            return GetNextMergeObjPrefab(mergeObjState - 1);
        }
    }
}