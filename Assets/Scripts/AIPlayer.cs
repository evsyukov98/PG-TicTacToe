using UnityEngine;

namespace TicTacToe
{
    public class AIPlayer : IPlayer
    {
        
        public TicTacState State { get; set; }
        
        public void MakeTurn(IPlayerReceiver model)
        {
            AiSelectCell(model, out var x, out var y);
            
            model.MakeTurn(State,x ,y);
        }
        
        private void AiSelectCell(IPlayerReceiver model, out int x, out int y)
        {
            var massLength = model.Grid.GetLength(1);
            
            x = Random.Range(0, massLength); 
            y = Random.Range(0, massLength);
            
            while (model.Grid[x,y] != TicTacState.None) 
            { 
                x = Random.Range(0, massLength); 
                y = Random.Range(0, massLength);
            }
        }
    }
}