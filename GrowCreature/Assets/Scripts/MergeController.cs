using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MergeController : MonoBehaviour
{
    public static MergeController Instance;

    public FetusSetSO fetusSetSo;
    public Button btnGenerate;
    public GameObject panelGrowComplete;
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
        InitFetus(fetus, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4, 4), 0));
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
        Instantiate(mergeEffectFX, fetus_1.transform.position, Quaternion.identity);

        //Merge & instantiate next object
        GameObject fetus = fetusSetSo.GetNextFetusPrefab(fetus_1.FetusState);
        InitFetus(fetus, fetus_1.transform.position);

        //Check for final state
        if (fetus.GetComponent<Fetus>().FetusState != 4) return;
        //Complete
        btnGenerate.gameObject.SetActive(false);

        StartCoroutine(ShowAllState());
    }

    private IEnumerator ShowAllState()
    {
        yield return new WaitForSeconds(2.0f);
        fetusHolder.gameObject.SetActive(false);
        for (int i = 0; i < fetusSetSo.AllFetusInfo.Count; i++)
        {
            GameObject fetus = fetusSetSo.GetFetusPrefab(i);
            fetus.GetComponent<Fetus>().enabled = false;
            fetus.transform.position = Vector3.zero;

            if (i == 4)
            {
                panelGrowComplete.SetActive(true);
                fetus.transform.rotation = Quaternion.Euler(0, 250, 0);
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
            Destroy(fetus);
        }
    }
}