using System;
using UnityEngine;

namespace RPGSystem
{
    [Serializable]
    public class SoundOptions
    {
        public bool keepPlayingWhenDisabled = false;
        public bool soundLoop = false;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Tooltip("0 -> 2D Sound (global)\n1 -> 3D Sound (from position)")]
        [Range(0f, 1f)]
        public float spatialBlend = 1f;
        [Range(-1f, 1f)]
        public float stereoPan = 0f;
        [Range(-3f, 3f)]
        public float pitch = 1f;
    }
}
