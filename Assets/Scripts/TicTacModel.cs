using System;
using System.Collections.Generic;
using UnityEngine;

public class TicTacModel
{
    
    public event Action<Vector2,TicTacState> StateChanged;
    public event Action<TicTacState> WinnerFound;

    private bool _isCross;

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

    public void SetState(Vector2 coordinate)
    {
        if (_isCross)
        {
            _grid[coordinate] = TicTacState.Cross;
            OnStateChanged(coordinate, TicTacState.Cross);
        }
        else
        {
            _grid[coordinate] = TicTacState.Noughts;
            OnStateChanged(coordinate, TicTacState.Noughts);
        }
    }
    
    private void OnStateChanged(Vector2 coordinate, TicTacState state)
    {
        StateChanged?.Invoke(coordinate, state);
        CheckWinner(coordinate);
        _isCross = !_isCross;
    }
    
    private void CheckWinner(Vector2 coordinate)
    {
        const int gridSize = 3;
        var currentState = _grid[coordinate];
        
        if ((int)coordinate.x == 1)
        {
            for (int i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate + new Vector2(i, 0)])
                {
                    break;
                }

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        if ((int)coordinate.y == 1)
        {
            for (int i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate + new Vector2(0, i)])
                {
                    break;
                }

                if (i != gridSize - 1) continue;
                
                WinnerFound?.Invoke(currentState);
                return;
            }
        }
        
        if ((int)coordinate.x == gridSize)
        {
            for (int i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate - new Vector2(i, 0)])
                { 
                    break;
                }

                if (i != gridSize - 1) continue;
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        if ((int) coordinate.y == gridSize) 
        {
            for (int i = 1; i < gridSize; i++)
            {
                if (currentState != _grid[coordinate - new Vector2(0, i)])
                {
                    break;
                }

                if (i != gridSize - 1) continue;
                WinnerFound?.Invoke(currentState);
                return;
            }
        }

        // Если точка помещена на диагоналях (11,22,33,13,31) % 2 = 0
        if ((coordinate.y + coordinate.x) % 2 != 0) return;
        
        if ((int)coordinate.x == (int)coordinate.y)
        {
            if (_grid[new Vector2(1, 1)] == _grid[new Vector2(2, 2)] &&
                _grid[new Vector2(2, 2)] == _grid[new Vector2(3, 3)])
            {
                WinnerFound?.Invoke(currentState);
            }
            return;
        }
            
        if (_grid[new Vector2(1, 3)] == _grid[new Vector2(2, 2)] &&
            _grid[new Vector2(2, 2)] == _grid[new Vector2(3, 1)])
        {
            WinnerFound?.Invoke(currentState);
        }
    }

    private void CheckWinner1(Vector2 coordinate)
    {
        if ((int)coordinate.x == 1)
        {
            if (_grid[coordinate] == _grid[coordinate + new Vector2(1, 0)] &&
                _grid[coordinate] == _grid[coordinate + new Vector2(2, 0)])
            {
                WinnerFound?.Invoke(_grid[coordinate]);
            }
        }
        
        if ((int)coordinate.y == 1)
        {
            if (_grid[coordinate] == _grid[coordinate + new Vector2(0, 1)] &&
                _grid[coordinate] == _grid[coordinate + new Vector2(0, 2)])
            {
                WinnerFound?.Invoke(_grid[coordinate]);
            }
        }
        
        if ((int)coordinate.x == 3)
        {
            if (_grid[coordinate] == _grid[coordinate - new Vector2(1, 0)] &&
                _grid[coordinate] == _grid[coordinate - new Vector2(2, 0)])
            {
                WinnerFound?.Invoke(_grid[coordinate]);
            }
        }
        
        if ((int)coordinate.y == 3)
        {
            if (_grid[coordinate] == _grid[coordinate - new Vector2(0, 1)] &&
                _grid[coordinate] == _grid[coordinate - new Vector2(0, 2)])
            {
                WinnerFound?.Invoke(_grid[coordinate]);
            }
        }
        
        // Если точка помещена на диагоналях (11,22,33,13,31) % 2 = 0
        if ((coordinate.y + coordinate.x) % 2 != 0) return;
        
        if ((int)coordinate.x == (int)coordinate.y)
        {
            if (_grid[new Vector2(1, 1)] == _grid[new Vector2(2, 2)] &&
                _grid[new Vector2(2, 2)] == _grid[new Vector2(3, 3)])
            {
                WinnerFound?.Invoke(_grid[coordinate]);
            }
            return;
        }
            
        if (_grid[new Vector2(1, 3)] == _grid[new Vector2(2, 2)] &&
            _grid[new Vector2(2, 2)] == _grid[new Vector2(3, 1)])
        {
            WinnerFound?.Invoke(_grid[coordinate]);
        }
    }
}
