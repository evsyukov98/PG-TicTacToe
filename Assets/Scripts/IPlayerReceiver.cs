namespace TicTacToe
{
    public interface IPlayerReceiver
    {
        
        TicTacState[,] Grid { get; }
        void MakeTurn(TicTacState state, int coordinateX, int coordinateY);
    }
}