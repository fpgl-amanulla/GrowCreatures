using UnityEngine;

public enum PouringPointType
{
    Left,
    Right
}

public class ConicalFlask : MonoBehaviour
{
    public LiquidType liquidType;
    public PouringPointType pouringPointType;
    public Transform conicalFlaskTr;
    public Renderer liquidRenderer;
    [HideInInspector] public Vector3 initialPos;

    private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

    private void Awake()
    {
        initialPos = conicalFlaskTr.position;
        liquidRenderer.material.SetFloat(FillAmount, .5f);
    }
}