﻿namespace Icarus.Core.Interfaces
{
    public interface IDrone : IDynamicLoadable
    {
        void Configure(DroneConfiguration.DroneConfiguration droneConfiguration);

        void Start();

        void Stop();

        void MoveLeft();

        void MoveRight();

        void MoveUp();

        void MoveDown();

        void MoveForward();

        void MoveBackward();

        void Hover();
    }
}