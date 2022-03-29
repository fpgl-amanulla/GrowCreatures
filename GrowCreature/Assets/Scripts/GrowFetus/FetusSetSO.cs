using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrowFetus
{
    [Serializable]
    public class FetusInfo
    {
        public int fetusState;
        public GameObject fetusPrefab;
    }

    [CreateAssetMenu(fileName = "FetusSet", menuName = "Create FetusSet", order = 0)]
    public class FetusSetSO : ScriptableObject
    {
        public List<FetusInfo> AllFetusInfo = new List<FetusInfo>();

        public GameObject GetNextFetusPrefab(int fetusState)
        {
            GameObject fetusPrefab = AllFetusInfo[fetusState + 1].fetusPrefab;
            GameObject fetusIns = Instantiate(fetusPrefab);
            Fetus fetus = fetusIns.GetComponent<Fetus>() == null
                ? fetusIns.AddComponent<Fetus>()
                : fetusIns.GetComponent<Fetus>();
            fetus.FetusState = AllFetusInfo[fetusState + 1].fetusState;

            if (fetusIns.GetComponent<CameraBoundaries>() == null)
                fetusIns.AddComponent<CameraBoundaries>();
            if (fetus.GetComponent<Rigidbody>() != null) return fetusIns.gameObject;

            Rigidbody rigidbody = fetusIns.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.mass = 1000;

            return fetusIns.gameObject;
        }

        public GameObject GetFetusPrefab(int fetusState)
        {
            return GetNextFetusPrefab(fetusState - 1);
        }
    }
}