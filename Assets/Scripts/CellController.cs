using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class CellController : MonoBehaviour
{
    
    public event Action<Vector2> CellSelected;

    [SerializeField] public Vector2 coordinate;
    
    [SerializeField] private Sprite cross = default;
    [SerializeField] private Sprite noughts = default;

    private Image _image;
    private Button _button;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnButtonClick()
    {
        CellSelected?.Invoke(coordinate);
    }

    public void CellStateChange(TicTacState state)
    {
        if (state == TicTacState.Cross)
        {
            _image.sprite = cross;
            _button.enabled = false;
        }
        else
        {
            _image.sprite = noughts;
            _button.enabled = false;
        }
    }
}
