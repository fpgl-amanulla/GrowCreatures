using System;
using UnityEngine;

public class MergeController : MonoBehaviour
{
    public static MergeController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    public bool IsMergePossible(Fetus fetus_1, Fetus fetus_2) => fetus_1.FetusState == fetus_2.FetusState;

    public void MergeObject(Fetus fetus_1, Fetus fetus_2)
    {
        //Play Merge Effect
        //Merge & instantiate next object
        
        //Check for final state
    }
}