using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TicTacPresenter : MonoBehaviour, IInputController
{

    [SerializeField] private Button hotSeatButton = default;
    [SerializeField] private Button withAIButton = default;
    
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
        _model.GameStatusChanged += OnGameStatusChanged;
        _model.PlayerMadeTurn += OnPlayerMadeTurn;
        _model.WinnerFound += Victory;

        hotSeatButton.onClick.AddListener(StartHotSeatGame);
        withAIButton.onClick.AddListener(StartWithAIGame);
        
        SetupCells();
        OnGameStatusChanged();
    }

    // Заблокировать кнопки если игра еще не началась.
    private void OnGameStatusChanged()
    {
        if (_model.IsGameStarted) return;
        
        foreach (var cell in _cellControllers.Values)
        {
            cell.SetActive(false);
        }
    }
    
    private void StartHotSeatGame()
    {
        IPlayer player1 = new LocalPlayer("Player_1", this);
        IPlayer player2 = new LocalPlayer("Player_2", this);

        _model.StartBattle(player1, player2);
    }

    private void StartWithAIGame()
    {
        IPlayer player = new LocalPlayer("Player", this);
        IPlayer ai = new AIPlayer("AI");

        _model.StartBattle(player, ai);
    }
    
    private void SetupCells()
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

    private void OnPlayerMadeTurn(Vector2 coordinate, TicTacState state)
    {
        _cellControllers[coordinate].CellStateChange(state);
    }

    private void Victory(TicTacState state)
    {
        winText.text = $"Wictory: {state}";
        _model.WinnerFound -= Victory;
        _model.PlayerMadeTurn -= OnPlayerMadeTurn;
        
        StartCoroutine(SceneController.RestartGame(2));
    }
}
