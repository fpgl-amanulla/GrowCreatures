using System;
using Core;
using DG.Tweening;
using Island;
using UnityEngine;

public class FocusSelectorResponse : MonoBehaviour, ISelectionResponse
{
    private Camera _camera;
    private Quaternion _camQuaternion;
    public ScrollAndPinch scrollAndPinch;

    private void Awake()
    {
        _camera = Camera.main;
        _camQuaternion = _camera!.transform.rotation;
    }

    public void OnSelect(Transform selection)
    {
        IsLandUI.Instance.ButtonHolderSetActive(false);
        _camera.transform.DOLookAt(selection.position, .5f);
        scrollAndPinch.PauseScroll();
        DOTween.To(() => _camera.fieldOfView, x => _camera.fieldOfView = x, 15, .5f);
    }

    public void OnDeselect(Transform selection)
    {
        _camera.transform.DORotateQuaternion(_camQuaternion, .5f);
        scrollAndPinch.StartScroll();
        DOTween.To(() => _camera.fieldOfView, x => _camera.fieldOfView = x, 40, 1.0f).OnComplete(() =>
        {
            IsLandUI.Instance.ButtonHolderSetActive(true);
        });
    }
}