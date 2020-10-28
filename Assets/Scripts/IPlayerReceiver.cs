using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public interface IPlayerReceiver
    {
        Dictionary<Vector2, TicTacState> Grid { get; }
        void MakeTurn(TicTacState state, Vector2 coordinate);
    }
}