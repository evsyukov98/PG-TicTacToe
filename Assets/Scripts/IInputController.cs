using System;

namespace TicTacToe
{
    public interface IInputController
    {
        
        Action<int, int> CellSelected { get; set; }
    }
}