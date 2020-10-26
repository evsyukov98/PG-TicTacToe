using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AiEnable : MonoBehaviour
{
    
    public event Action<bool> AIEnable;

    [SerializeField] private bool enable = default;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnButtonClick);
    }

    public void DisActivate()
    {
        gameObject.SetActive(false);
    }
    
    private void OnButtonClick()
    {
        AIEnable?.Invoke(enable);
    }
}