using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacPresenter : MonoBehaviour
{

    [SerializeField] private AiEnable aiOff = default;
    [SerializeField] private AiEnable aiOn = default;
    
    [SerializeField] private Text winText = default;

    private Dictionary<Vector2, CellController> _cellControllers;
    
    private readonly TicTacModel _model = new TicTacModel();

    private bool _aIEnabled;
    
    private void Awake()
    {
        _cellControllers =  new Dictionary<Vector2, CellController>(9);
    }

    private void Start()
    {
        _model.StateChanged += CellStateChanged;
        _model.WinnerFound += Victory;
        
        aiOff.AIEnable += AiEnable;
        aiOn.AIEnable += AiEnable;
    }

    private void AiEnable(bool enable)
    {
        _aIEnabled = enable;
        
        aiOff.DisActivate();
        aiOn.DisActivate();
        
        GetCellControllers();
    }

    private void GetCellControllers()
    {
        var cellControllersMass = gameObject.GetComponentsInChildren<CellController>();
        
        foreach (var cell in cellControllersMass)
        {
            _cellControllers.Add(cell.coordinate, cell);
            cell.CellSelected += OnCellSelected;
        }
    }
    
    private void OnCellSelected(Vector2 coordinate)
    {
        _model.SetState(coordinate);

        if (_aIEnabled)
        {
            _model.AiSetState();
        }
    }

    private void CellStateChanged(Vector2 coordinate, TicTacState state)
    {
        _cellControllers[coordinate].CellStateChange(state);
    }

    private void Victory(TicTacState state)
    {
        winText.text = $"Wictory: {state}";
        _model.WinnerFound -= Victory;
        _model.StateChanged -= CellStateChanged;
        
        StartCoroutine(SceneController.RestartGame(2));
    }
}
