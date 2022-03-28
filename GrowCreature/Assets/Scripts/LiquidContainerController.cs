using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainerController : MonoBehaviour
{
    public Transform baseContainer;
    public LiquidDummy liquidDummyPrefab;

    private bool startPouring = false;
    private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

    private List<LiquidDummy> allLiquidDummy = new List<LiquidDummy>();
    private LiquidDummy _liquidDummy;
    private Renderer _liquidRenderer;
    private float fillAmount = 1.25f;

    public ParticleSystem cfx_magical_source;
    public GameObject fetus;
    private static readonly int TopColor = Shader.PropertyToID("_TopColor");

    private void Update()
    {
        if (!startPouring) return;
        if (fillAmount < 0) return;
        //Update value
        fillAmount -= .001f;
        _liquidRenderer.material.SetFloat(FillAmount, fillAmount);
        if (fillAmount <= 0)
        {
            Debug.Log("Filled");
            cfx_magical_source.gameObject.SetActive(true);
            StartCoroutine(GenerateFetus());
        }
    }

    private IEnumerator GenerateFetus()
    {
        yield return new WaitForSeconds(2.0f);
        fetus.SetActive(true);
        cfx_magical_source.gameObject.SetActive(false);
        DeactivateAllLiquidDummy();
    }

    private void DeactivateAllLiquidDummy()
    {
        for (int i = 0; i < allLiquidDummy.Count; i++)
        {
            allLiquidDummy[i].gameObject.SetActive(false);
        }
    }

    public void StartPourLiquid(bool status, ConicalFlask conicalFlask)
    {
        if (_liquidDummy != null)
            if (_liquidDummy.LiquidType == conicalFlask.liquidType)
            {
                startPouring = true;
                return;
            }

        Renderer previousRenderer = null;
        if (_liquidRenderer != null)
        {
            previousRenderer = _liquidRenderer;
        }

        _liquidDummy = Instantiate(liquidDummyPrefab, baseContainer);
        allLiquidDummy.Add(_liquidDummy);
        _liquidDummy.LiquidType = conicalFlask.liquidType;
        _liquidRenderer = _liquidDummy.GetComponent<Renderer>();
        _liquidRenderer.material = conicalFlask.liquidRenderer.material;
        int index = allLiquidDummy.Count - 3;
        if (index >= 0)
        {
            allLiquidDummy[index].GetComponent<Renderer>().material
                .SetColor(TopColor, _liquidRenderer.material.GetColor(TopColor));
        }

        _liquidRenderer.material.SetFloat(FillAmount, fillAmount);
        if (previousRenderer != null)
            previousRenderer.material.SetColor(TopColor, _liquidRenderer.material.GetColor(TopColor));

        startPouring = true;
    }

    public void StopPourLiquid()
    {
        startPouring = false;
    }
}