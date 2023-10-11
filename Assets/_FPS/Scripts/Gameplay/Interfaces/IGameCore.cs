using System;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public interface IGameCore
    {
        public event Action GameRestarted;
    }
}