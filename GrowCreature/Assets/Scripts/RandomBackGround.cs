using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomBackGround : MonoBehaviour
{
    private Renderer _renderer;

    public List<Texture2D> AllTexture2D = new List<Texture2D>();

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = AllTexture2D[Random.Range(0, AllTexture2D.Count)];
    }
}