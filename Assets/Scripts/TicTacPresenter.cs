using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacPresenter : MonoBehaviour
{

    [SerializeField] private Text winText = default;

    private Dictionary<Vector2, CellController> _cellControllers;
    
    private readonly TicTacModel _model = new TicTacModel();

    private void Awake()
    {
        _cellControllers =  new Dictionary<Vector2, CellController>(9);
        GetCellController();    
    }

    private void Start()
    {
        foreach (var cell in _cellControllers)
        {
            cell.Value.CellSelected += OnCellSelected;
        }

        _model.StateChanged += CellStateChanged;
        _model.WinnerFound += WinText;
    }

    private void GetCellController()
    {
        CellController[] cells = gameObject.GetComponentsInChildren<CellController>();
        
        foreach (var cell in cells)
        {
            _cellControllers.Add(cell.coordinate, cell);
        }
    }
    
    private void OnCellSelected(Vector2 coordinate)
    {
        _model.SetState(coordinate);
    }

    private void CellStateChanged(Vector2 coordinate, TicTacState state)
    {
        _cellControllers[coordinate].CellStateChange(state);
    }

    private void WinText(TicTacState state)
    {
        winText.text = $"Wictory: {state}";
    }
}
