using UnityEngine;

namespace TicTacToe
{
    public class AIPlayer : IPlayer
    {
        private readonly string _name;

        public AIPlayer(string name)
        {
            _name = name;
        }

        public TicTacState State { get; set; }
        
        public void MakeTurn(IPlayerReceiver model)
        {
            model.MakeTurn(State, AiSelectCell(model));
        }
        
        private Vector2 AiSelectCell(IPlayerReceiver model) 
        {
            var x = Random.Range(1, 4); 
            var y = Random.Range(1, 4);
            
            var aiCoordinate = new Vector2(x,y);
            
            while (model.Grid[aiCoordinate] != TicTacState.None) 
            { 
                x = Random.Range(1, 4); 
                y = Random.Range(1, 4);
                
                aiCoordinate = new Vector2(x,y); 
            }

            return aiCoordinate;
        }
    }
}