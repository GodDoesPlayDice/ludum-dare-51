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

        public void PlaySfxAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
        {
            if (CurrentSoundMode is SoundModes.SfxOnly or SoundModes.MusicPlusSfx)
                AudioSource.PlayClipAtPoint(clip, position, volume);
        }

        public void SetSoundMode(int soundMode)
        {
            CurrentSoundMode = (SoundModes) soundMode;
            sfxSource.mute = CurrentSoundMode is SoundModes.None or SoundModes.MusicOnly;
            musicSource.mute = CurrentSoundMode is SoundModes.None or SoundModes.SfxOnly;
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