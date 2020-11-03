using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class TicTacPresenter : MonoBehaviour, IInputController
    {

        [SerializeField] private Button hotSeatButton = default;
        [SerializeField] private Button withAIButton = default;
        [SerializeField] private Text winText = default;

        private Dictionary<Vector2, CellController> _cellControllers;

        private readonly TicTacModel _model = new TicTacModel();
        
        public Action<int, int> CellSelected { get; set; }

        private void Awake()
        {
            _cellControllers = new Dictionary<Vector2, CellController>(9);
        }

        private void Start()
        {
            _model.GameStatusChanged += OnGameStatusChanged;
            _model.PlayerMadeTurn += OnPlayerMadeTurn;
            _model.WinnerFound += OnWinnerFound;

            hotSeatButton.onClick.AddListener(StartHotSeatGame);
            withAIButton.onClick.AddListener(StartWithAIGame);

            SetupCells();
            OnGameStatusChanged();
        }

        private void OnGameStatusChanged()
        {
            foreach (var cell in _cellControllers.Values)
            {
                cell.SetActive(_model.IsGameStarted);
            }
        }

        private void StartHotSeatGame()
        {
            IPlayer player1 = new LocalPlayer(this);
            IPlayer player2 = new LocalPlayer( this);

            _model.StartBattle(player1, player2);
            hotSeatButton.enabled = false;
        }

        private void StartWithAIGame()
        {
            IPlayer player = new LocalPlayer(this);
            IPlayer ai = new AIPlayer();

            _model.StartBattle(player, ai);
            hotSeatButton.enabled = false;
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
            CellSelected?.Invoke((int)coordinate.x ,(int)coordinate.y);
        }

        private void OnPlayerMadeTurn(TicTacState state, int coordinateX,int coordinateY)
        {
            _cellControllers[new Vector2(coordinateX, coordinateY)].CellStateChange(state);
        }

        private void OnWinnerFound(TicTacState state)
        {
            winText.text = $"Victory: {state}";
            
            _model.WinnerFound -= OnWinnerFound;
            _model.PlayerMadeTurn -= OnPlayerMadeTurn;

            StartCoroutine(SceneController.RestartGame(2));
        }
    }
}
