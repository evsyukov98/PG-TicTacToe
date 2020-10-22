using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    public TicTac currentState = TicTac.None;
    [SerializeField] private Sprite cross;
    [SerializeField] private Sprite noughts;
    
    private Image _image;

    // true - cross turn, false - noughts turn.
    public bool isCross;
    public bool isBlocked = false;

    [SerializeField] private Presenter presenter;
    private void Awake()
    {
        _image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(ChangeButton);
    }

    private void ChangeButton()
    {
        if (isBlocked)
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
            return;
        }
        if (isCross)
        {
            _image.sprite = cross;
            currentState = TicTac.Cross;
            isBlocked = true;
        }
        else
        {
            _image.sprite = noughts;
            currentState = TicTac.Noughts;
            isBlocked = true;
        }
    }
}
