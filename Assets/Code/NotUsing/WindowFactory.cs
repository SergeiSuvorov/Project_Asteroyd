using System;
using UnityEngine;

namespace Asteroids.Abstrac_Factory
{
    internal sealed class WindowFactory
    {
        public IWindow CreateWindow(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return new PCWindow();
                case RuntimePlatform.XBOX360:
                case RuntimePlatform.PS3:
                    return new ConsoleWindow();
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }
        }
    }
}
