using UnityEngine;
using UnityEngine.Serialization;

public class Fetus : MonoBehaviour
{
    [SerializeField] private int _fetusState;

    public int FetusState
    {
        get => _fetusState;
        set => _fetusState = value;
    }
}