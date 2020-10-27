namespace DefaultNamespace
{
    public interface IPlayer
    {
        TicTacState State { get; set; }
        void MakeTurn(IPlayerInput input);
    }
}