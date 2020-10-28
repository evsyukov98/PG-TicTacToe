using System;
using UnityEngine;

namespace TicTacToe
{
    public interface IInputController
    {
        Action<Vector2> CellSelected { get; set; }
    }
}