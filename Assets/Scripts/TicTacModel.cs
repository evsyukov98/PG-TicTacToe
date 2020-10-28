using System;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class TicTacModel : IPlayerReceiver
    {

        private int _turnCount;

        private IPlayer _player1;
        private IPlayer _player2;

        private IPlayer _activePlayer;

        private bool _isGameRunning;

        public Dictionary<Vector2, TicTacState> Grid { get; private set; } = new Dictionary<Vector2, TicTacState>
        {
            {new Vector2(1, 1), TicTacState.None},
            {new Vector2(1, 2), TicTacState.None},
            {new Vector2(1, 3), TicTacState.None},
            {new Vector2(2, 1), TicTacState.None},
            {new Vector2(2, 2), TicTacState.None},
            {new Vector2(2, 3), TicTacState.None},
            {new Vector2(3, 1), TicTacState.None},
            {new Vector2(3, 2), TicTacState.None},
            {new Vector2(3, 3), TicTacState.None},
        };

        // TODO: Change Dictionary to Mass 
        // public int[,] gridd = new int[3,3];

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
        public event Action<Vector2, TicTacState> PlayerMadeTurn;
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


        void IPlayerReceiver.MakeTurn(TicTacState state, Vector2 coordinate)
        {
            SetState(state, coordinate);

            if (_turnCount <= 0)
            {
                _isGameRunning = false;
                return;
            }

            _turnCount--;

            _activePlayer = _activePlayer == _player1 ? _player2 : _player1;

            if (_isGameRunning) _activePlayer.MakeTurn(this);
        }

        private void SetState(TicTacState state, Vector2 coordinate)
        {
            Grid[coordinate] = state;

            PlayerMadeTurn?.Invoke(coordinate, state);

            CheckWinner(coordinate);
        }

        private void CheckWinner(Vector2 coordinate)
        {
            const int gridSize = 3;
            var currentState = Grid[coordinate];

            if ((int) coordinate.x == 1)
            {
                for (var i = 1; i < gridSize; i++)
                {
                    if (currentState != Grid[coordinate + new Vector2(i, 0)]) break;

                    if (i != gridSize - 1) continue;

                    WinnerFound?.Invoke(currentState);
                    return;
                }
            }

            if ((int) coordinate.y == 1)
            {
                for (var i = 1; i < gridSize; i++)
                {
                    if (currentState != Grid[coordinate + new Vector2(0, i)]) break;

                    if (i != gridSize - 1) continue;

                    WinnerFound?.Invoke(currentState);
                    return;
                }
            }

            if ((int) coordinate.x == gridSize)
            {
                for (var i = 1; i < gridSize; i++)
                {
                    if (currentState != Grid[coordinate - new Vector2(i, 0)]) break;

                    if (i != gridSize - 1) continue;

                    WinnerFound?.Invoke(currentState);
                    return;
                }
            }

            if ((int) coordinate.y == gridSize)
            {
                for (var i = 1; i < gridSize; i++)
                {
                    if (currentState != Grid[coordinate - new Vector2(0, i)]) break;

                    if (i != gridSize - 1) continue;

                    WinnerFound?.Invoke(currentState);
                    return;
                }
            }

            // Если точка помещена на диагоналях (11,22,33,13,31) % 2 = 0
            if ((coordinate.y + coordinate.x) % 2 != 0) return;

            if ((int) coordinate.x == (int) coordinate.y)
            {
                if (Grid[new Vector2(1, 1)] != Grid[new Vector2(2, 2)] ||
                    Grid[new Vector2(2, 2)] != Grid[new Vector2(3, 3)]) return;

                WinnerFound?.Invoke(currentState);
                return;
            }

            if (Grid[new Vector2(1, 3)] != Grid[new Vector2(2, 2)] ||
                Grid[new Vector2(2, 2)] != Grid[new Vector2(3, 1)]) return;

            WinnerFound?.Invoke(currentState);
        }
    }
}
