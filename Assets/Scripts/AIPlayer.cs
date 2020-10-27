namespace DefaultNamespace
{
    public class AIPlayer : IPlayer
    {
        
        public readonly string Name;

        public AIPlayer(string name)
        {
            Name = name;
        }

        public TicTacState State { get; set; }
        public void MakeTurn(IPlayerInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}