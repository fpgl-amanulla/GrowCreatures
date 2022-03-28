using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MergeController : MonoBehaviour
{
    public static MergeController Instance;

    public FetusSetSO fetusSetSo;
    public Button btnGenerate;
    public Transform fetusHolder;
    public GameObject mergeEffectFX;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        btnGenerate.onClick.AddListener(GenerateFetus);
    }

    private void Start()
    {
        fetusSetSo.GetFetusPrefab(0);
    }

    private void GenerateFetus()
    {
        GameObject fetus = fetusSetSo.GetFetusPrefab(0);
        InitFetus(fetus, new Vector3(Random.Range(-3, 3), Random.Range(-5, 5), 0));
    }

    private void InitFetus(GameObject fetus, Vector3 position)
    {
        fetus.transform.SetParent(fetusHolder);
        fetus.transform.position = position;
    }

    public bool IsMergePossible(Fetus fetus_1, Fetus fetus_2) => fetus_1.FetusState == fetus_2.FetusState;

    public void MergeObject(Fetus fetus_1)
    {
        //Play Merge Effect

        //Merge & instantiate next object
        GameObject fetus = fetusSetSo.GetNextFetusPrefab(fetus_1.FetusState);
        InitFetus(fetus, fetus_1.transform.position);
        Instantiate(mergeEffectFX, fetus_1.transform.position, Quaternion.identity);

        //Check for final state
    }
}