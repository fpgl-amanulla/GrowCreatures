using System;
using System.Collections.Generic;
using UnityEngine;

namespace Merge
{
    [Serializable]
    public class FetusInfo
    {
        public int fetusState;
        public GameObject fetusPrefab;
        public Texture2D mainTexture2D;
    }

    [CreateAssetMenu(fileName = "FetusSet", menuName = "Create FetusSet", order = 0)]
    public class FetusSetSO : ScriptableObject
    {
        public List<FetusInfo> AllFetusInfo = new List<FetusInfo>();

        public GameObject GetNextFetusPrefab(int fetusState)
        {
            int stateIndex = fetusState + 1;
            GameObject fetusPrefab = AllFetusInfo[stateIndex].fetusPrefab;
            GameObject fetusIns = Instantiate(fetusPrefab);
            fetusIns.GetComponentInChildren<Renderer>().material.mainTexture = AllFetusInfo[stateIndex].mainTexture2D;
            Fetus fetus = fetusIns.GetComponent<Fetus>() == null
                ? fetusIns.AddComponent<Fetus>()
                : fetusIns.GetComponent<Fetus>();
            fetus.FetusState = AllFetusInfo[stateIndex].fetusState;

            if (fetusIns.GetComponent<CameraBoundaries>() == null)
                fetusIns.AddComponent<CameraBoundaries>();

            if (fetusIns.GetComponent<Rigidbody>() == null) fetusIns.AddComponent<Rigidbody>();
            Rigidbody rigidbody = fetusIns.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.drag = 1000;

            return fetusIns.gameObject;
        }

        public GameObject GetFetusPrefab(int fetusState)
        {
            return GetNextFetusPrefab(fetusState - 1);
        }
    }
}