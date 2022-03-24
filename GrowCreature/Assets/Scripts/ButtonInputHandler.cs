using UnityEngine;
using UnityEngine.Events;

public class ButtonInputHandler : MonoBehaviour
{
    public static ButtonInputHandler Instance;
    public UnityAction<ButtonLiquidHandler, bool> OnButtonPointerChanges;

    private ButtonLiquidHandler _currentLiquidHandler;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this.gameObject);
    }

    public void OnPointerDown(ButtonLiquidHandler liquidHandler)
    {
        _currentLiquidHandler = liquidHandler;
        OnButtonPointerChanges?.Invoke(_currentLiquidHandler, true); // Pointer Down true
    }

    public void OnPointerUp() =>
        OnButtonPointerChanges?.Invoke(_currentLiquidHandler, false); //// Pointer Down false
}