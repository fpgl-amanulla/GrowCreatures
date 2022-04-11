using UnityEngine;

public abstract class SelectionManager : MonoBehaviour
{
    protected IRayProvider _rayProvider;
    protected ISelector _selector;
    protected ISelectionResponse _selectionResponse;

    protected Transform _currentSelection;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    public virtual void Update()
    {
        if (_currentSelection != null)
            _selectionResponse.OnDeselect(_currentSelection);

        _selector.Check(_rayProvider.CreateRay());
        _currentSelection = _selector.GetSelection();

        if (_currentSelection != null)
            _selectionResponse.OnSelect(_currentSelection);
    }
}