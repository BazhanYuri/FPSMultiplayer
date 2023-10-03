using System;
using Unity.FPS.Enums;

namespace Unity.FPS.Multiplayer
{
    public interface IPhotonManager
    {
        public int BlueTeamPlayerCount { get; }
        public int RedTeamPlayerCount { get; }

        public event Action TeamCountUpdated;
        void AddPlayerToTeam(TeamType teamType, Player player);
    }
}

