using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;

        [SerializeField] private AudioSource musicSource;
        // [SerializeField] private AudioMixer mixer;

        [field: SerializeField] public SoundModes CurrentSoundMode { get; private set; }

        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            CurrentSoundMode = SoundModes.MusicPlusSfx;
        }

        public void PlayAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }

        public void SetSoundMode(int soundMode)
        {
            CurrentSoundMode = (SoundModes) soundMode;
        }
    }

    public enum SoundModes
    {
        MusicPlusSfx,
        MusicOnly,
        SfxOnly,
        None
    }
}