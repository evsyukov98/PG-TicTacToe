using System;

namespace TicTacToe
{
    public class LocalPlayer : IPlayer
    {
        
        private readonly IInputController _inputController;

        private IPlayerReceiver _model;
        
        public TicTacState State { get; set; }
        

        public LocalPlayer(IInputController inputController)
        {
            _inputController = inputController ?? throw new NullReferenceException(nameof(inputController));
        }

        public void MakeTurn(IPlayerReceiver model)
        {
            _model = model;
            _inputController.CellSelected += SelectCell;
        }
        
        private void SelectCell(int coordinateX, int coordinateY)
        {
            _model.MakeTurn(State, coordinateX, coordinateY);

            if (_inputController.CellSelected != null) _inputController.CellSelected -= SelectCell;
        }
    }
}