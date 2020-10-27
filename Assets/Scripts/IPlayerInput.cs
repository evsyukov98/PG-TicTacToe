using UnityEngine;

namespace DefaultNamespace
{
    public interface IPlayerInput
    {

        bool MakeTurn(IPlayer player, Vector2 coords);
    }
}