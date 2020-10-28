namespace TicTacToe
{
    public interface IPlayer
    {
        TicTacState State { get; set; }
        void MakeTurn(IPlayerReceiver input);
    }
}