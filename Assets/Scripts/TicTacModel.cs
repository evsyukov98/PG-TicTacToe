using System;

namespace TicTacToe
{
    public class TicTacModel : IPlayerReceiver
    {

        private int _turnCount;

        private IPlayer _player1;
        private IPlayer _player2;

        private IPlayer _activePlayer;

        private bool _isGameRunning;

        public TicTacState[,] Grid { get; private set; } = new TicTacState[3,3];

        public bool IsGameStarted
        {
            get => _isGameRunning;
            private set
            {
                if (_isGameRunning == value) return;
                _isGameRunning = value;
                GameStatusChanged?.Invoke();
            }
        }

        public event Action GameStatusChanged;
        public event Action<TicTacState, int, int> PlayerMadeTurn;
        public event Action<TicTacState> WinnerFound;

        public void StartBattle(IPlayer player1, IPlayer player2)
        {
            if (IsGameStarted) return;
            
            _turnCount = 8;

            _player1 = player1 ?? throw new NullReferenceException(nameof(player1));
            _player2 = player2 ?? throw new NullReferenceException(nameof(player2));

            _player1.State = TicTacState.Cross;
            _player2.State = TicTacState.Noughts;

            _activePlayer = _player1;
            IsGameStarted = true;

            _activePlayer.MakeTurn(this);
        }


        void IPlayerReceiver.MakeTurn(TicTacState state, int coordinateX, int coordinateY)
        {
            SetState(state, coordinateX, coordinateY);

            if (_turnCount <= 0)
            {
                _isGameRunning = false;
                return;
            }

            _turnCount--;

            _activePlayer = _activePlayer == _player1 ? _player2 : _player1;

            if (_isGameRunning) _activePlayer.MakeTurn(this);
        }

        private void SetState(TicTacState state, int coordinateX, int coordinateY)
        { 
            Grid[coordinateX,coordinateY] = state;

            PlayerMadeTurn?.Invoke(state, coordinateX, coordinateY);

            CheckWinner(coordinateX, coordinateY);
        }

        private void CheckWinner(int coordinateX, int coordinateY)
        {
            var gridSize = Grid.GetLength(1);
            
            var currentState = Grid[coordinateX,coordinateY];
            
            for (var i = 0; i < gridSize; i++)
            {
                if (currentState != Grid[coordinateX, 0 +  i]) break;

                if (i != gridSize - 1) continue;

                WinnerFound?.Invoke(currentState);
                return;
            }
                
            for (var i = 0; i < gridSize; i++)
            {
                if (currentState != Grid[0 + i, coordinateY]) break;

                if (i != gridSize - 1) continue;

                WinnerFound?.Invoke(currentState);
                return;
            }

            if(Grid[1,1] == 0) return;
            
            if ((coordinateY + coordinateX) % 2 != 0) return;

            if (Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2] ||
                Grid[2, 0] == Grid[1, 1] && Grid[1, 1] == Grid[0, 2])
            {
                WinnerFound?.Invoke(currentState);
            }
        }
    }
}
