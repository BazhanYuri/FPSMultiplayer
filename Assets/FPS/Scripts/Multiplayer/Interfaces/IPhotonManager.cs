using System;
using Unity.FPS.Enums;

namespace Unity.FPS.Multiplayer
{
    public interface IPhotonManager
    {
        public TeamScoreHanlder TeamScoreHanlder { get; }
        public int BlueTeamPlayerCount { get; }
        public int RedTeamPlayerCount { get; }

        public event Action TeamCountUpdated;
        public event Action<TeamType> TeamWon;
        void AddPlayerToTeam(TeamType teamType, Player player);
    }
}

