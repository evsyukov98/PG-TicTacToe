using System;

namespace DefaultNamespace
{
    public class LocalPlayer : IPlayer
    {
        
        public readonly string Name;
        
        private readonly IInputController _inputController;
        TicTacState IPlayer.State { get; set; }

        public LocalPlayer(string name, IInputController inputController)
        {
            Name = name;
            _inputController = inputController ?? throw new NullReferenceException(nameof(inputController));
            //TODO
            // _inputController.OnCellSelected += 
        }
        
        void IPlayer.MakeTurn(IPlayerInput input)
        {
            throw new NotImplementedException();
        }
        
    }
}