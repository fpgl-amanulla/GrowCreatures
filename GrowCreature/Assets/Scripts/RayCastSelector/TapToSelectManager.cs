using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class TapToSelectManager : SelectionManager
{
    public Button btnCross;
    
    private void Start()
    {
        btnCross.onClick.AddListener(CrossCallBack);
    }

    private void CrossCallBack()
    {
        btnCross.gameObject.SetActive(false);
        _selectionResponse.OnDeselect(_currentSelection);
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selector.Check(_rayProvider.CreateRay());
            _currentSelection = _selector.GetSelection();

            if (_currentSelection != null)
            {
                btnCross.gameObject.SetActive(true);
                _selectionResponse.OnSelect(_currentSelection);
            }
        }
    }
}