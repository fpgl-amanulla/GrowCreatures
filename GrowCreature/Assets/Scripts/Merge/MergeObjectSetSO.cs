using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Merge
{
    [Serializable]
    public class MergeObjectInfo
    {
        public int productId;
        public int mergeObjState;

        [ShowAssetPreview(32, 32)] [FormerlySerializedAs("fetusPrefab")]
        public GameObject mergeObjPrefab;

        [ShowAssetPreview(32, 32)] public Texture2D mainTexture2D;
    }

    [CreateAssetMenu(fileName = "MergeObjectSet", menuName = "MergeObject/Create MergeObjectSet", order = 0)]
    public class MergeObjectSetSO : ScriptableObject
    {
        [FormerlySerializedAs("AllFetusInfo")]
        public List<MergeObjectInfo> AllMergeObjInfo = new List<MergeObjectInfo>();

        public GameObject GetNextMergeObjPrefab(int mergeObjState, bool init = true)
        {
            int stateIndex = mergeObjState + 1;
            GameObject mergeObjPrefab = AllMergeObjInfo[stateIndex].mergeObjPrefab;
            GameObject mergeObjIns = Instantiate(mergeObjPrefab);
            List<Renderer> renderers = mergeObjIns.GetComponentsInChildren<Renderer>().ToList();
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material.mainTexture =
                    AllMergeObjInfo[stateIndex].mainTexture2D;
            }

            if (!init) return mergeObjIns;

            MergeObject mergeObject = mergeObjIns.GetComponent<MergeObject>() == null
                ? mergeObjIns.AddComponent<MergeObject>()
                : mergeObjIns.GetComponent<MergeObject>();
            mergeObject.MergeObjectState = AllMergeObjInfo[stateIndex].mergeObjState;

            if (stateIndex == AllMergeObjInfo.Count - 1) return mergeObjIns;

            if (mergeObjIns.GetComponent<CameraBoundaries>() == null)
            {
                CameraBoundaries cameraBoundaries = mergeObjIns.AddComponent<CameraBoundaries>();
                cameraBoundaries.SetBound(1.5f);
            }

            if (mergeObjIns.GetComponent<Rigidbody>() == null) mergeObjIns.AddComponent<Rigidbody>();
            Rigidbody rigidbody = mergeObjIns.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.drag = 1000;

            return mergeObjIns.gameObject;
        }

        public GameObject GetMergeObjPrefab(int mergeObjState, bool init = true)
        {
            return GetNextMergeObjPrefab(mergeObjState - 1, init);
        }
    }
}