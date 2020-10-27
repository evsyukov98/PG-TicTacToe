using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class TicTacModel : IPlayerInput
{

    private int _turnCount = 9;

    private IPlayer _player1;
    private IPlayer _player2;

    private IPlayer _activePlayer;
    
    private bool _isGameRunning;
    
    private readonly Dictionary<Vector2, TicTacState> _grid = new Dictionary<Vector2, TicTacState>
    {
        {new Vector2(1,1), TicTacState.None},
        {new Vector2(1,2), TicTacState.None},
        {new Vector2(1,3), TicTacState.None},
        {new Vector2(2,1), TicTacState.None},
        {new Vector2(2,2), TicTacState.None},
        {new Vector2(2,3), TicTacState.None},
        {new Vector2(3,1), TicTacState.None},
        {new Vector2(3,2), TicTacState.None},
        {new Vector2(3,3), TicTacState.None},
    };

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
    public event Action<Vector2,TicTacState> PlayerMadeTurn;
    public event Action<TicTacState> WinnerFound;
    
    public void StartBattle(IPlayer player1, IPlayer player2)
    {
        if (IsGameStarted) return;
        
        _player1 = player1 ?? throw new NullReferenceException(nameof(player1));
        _player2 = player2 ?? throw new NullReferenceException(nameof(player2));

        _player1.State = TicTacState.Cross;
        _player2.State = TicTacState.Noughts;

        _activePlayer = _player1;
        IsGameStarted = true;

        _player1.MakeTurn(this);
    }
    
    /*public void AiSetState()
    {
        if(_turnCount <= 0) return;
        
        var x = Random.Range(1, 4);
        var y = Random.Range(1, 4);

        var aiCoordinate = new Vector2(x,y);
        
        while (_grid[aiCoordinate] != TicTacState.None)
        {
            x = Random.Range(1, 4);
            y = Random.Range(1, 4);
            
            aiCoordinate = new Vector2(x,y);
        }
        
        SetState(aiCoordinate);
    }*/

    
    bool IPlayerInput.MakeTurn(IPlayer player, Vector2 coords)
    {
        if (player is null || player != _activePlayer) return false;
        
        SetState(player.State, coords);

        _activePlayer = _activePlayer == _player1 ? _player2 : _player1;
        if (_isGameRunning) _activePlayer.MakeTurn(this);
        return true;
    }

    private void SetState(TicTacState state, Vector2 coordinate)
    {
        _turnCount--;
        
        _grid[coordinate] = state;
        PlayerMadeTurn?.Invoke(coordinate, state);
        CheckWinner(coordinate);
    }

    private void CheckWinner(Vector2 coordinate)
    {
        const int gridSize = 3;
        var currentState = _grid[coordinate];
        
        if ((int)coordinate.x == 1)
        {
            for (var i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate + new Vector2(i, 0)]) break;

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        if ((int)coordinate.y == 1)
        {
            for (var i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate + new Vector2(0, i)]) break;

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }
        
        if ((int)coordinate.x == gridSize)
        {
            for (var i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate - new Vector2(i, 0)]) break;

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        if ((int) coordinate.y == gridSize) 
        {
            for (var i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate - new Vector2(0, i)]) break;

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        // Если точка помещена на диагоналях (11,22,33,13,31) % 2 = 0
        if ((coordinate.y + coordinate.x) % 2 != 0) return;
        
        if ((int)coordinate.x == (int)coordinate.y)
        {
            if (_grid[new Vector2(1, 1)] != _grid[new Vector2(2, 2)] ||
                _grid[new Vector2(2, 2)] != _grid[new Vector2(3, 3)]) return;
            
            WinnerFound?.Invoke(currentState);
            return;
        }

        if (_grid[new Vector2(1, 3)] != _grid[new Vector2(2, 2)] ||
            _grid[new Vector2(2, 2)] != _grid[new Vector2(3, 1)]) return;
        
        WinnerFound?.Invoke(currentState);
    }

}
