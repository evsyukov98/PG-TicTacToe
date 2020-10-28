using System;
using UnityEngine;

namespace TicTacToe
{
    public class LocalPlayer : IPlayer
    {
        private readonly string _name;
        
        private readonly IInputController _inputController;

        private IPlayerReceiver _model;
        public TicTacState State { get; set; }

        public LocalPlayer(string name, IInputController inputController)
        {
            _name = name;
            _inputController = inputController ?? throw new NullReferenceException(nameof(inputController));

        }

        public void MakeTurn(IPlayerReceiver model)
        {
            _model = model;
            _inputController.CellSelected += SelectCell;
        }

        private void SelectCell(Vector2 coordinate)
        {
            _model.MakeTurn(State,coordinate);

            if (_inputController.CellSelected != null) _inputController.CellSelected -= SelectCell;
        }
        
    }
}