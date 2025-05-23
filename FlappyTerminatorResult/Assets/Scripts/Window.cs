using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Window))]
public class Window : MonoBehaviour
{
    [SerializeField] private Button _actionButton;

    private CanvasGroup _windowGroup;

    public event Action ButtonClicked;

    private void Awake()
    {
        _windowGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    public  void Open()
    {
        _windowGroup.alpha = 1f;
        _actionButton.interactable = true;
    }

    public  void Close()
    {
        _windowGroup.alpha = 0f;
        _actionButton.interactable = false;
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
