using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] private ButtonView _buttonPrefab;
    [SerializeField] private GameObject winText;

    private ButtonView[,] _buttonsViews;
    
    private Model _model = new Model();
    
    

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _buttonsViews = new ButtonView[3,3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _buttonsViews[i, j] = Instantiate(_buttonPrefab, transform);
                _buttonsViews[i,j].GetComponent<Button>().onClick.AddListener(OnButtonClick);
                _buttonsViews[i,j].GetComponent<Button>().onClick.AddListener(CheckWinner);
            }
        }
    }
    
    private void OnButtonClick()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // собрать все данные отправить в Model
                _model.Grid[i,j] = _buttonsViews[i, j].currentState;
                
                // поменять крестик <-> нолик
                _buttonsViews[i, j].isCross = !(_buttonsViews[i, j].isCross);
            }
        }
    }
    
    private void CheckWinner()
    {
        if (_model.GetWinner())
        {
            winText.SetActive(true);
        }
    }

}
