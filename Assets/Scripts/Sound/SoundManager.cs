using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public AudioClip menuMusic;
        public AudioClip gameplayMusic;

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource01;
        [SerializeField] private AudioSource musicSource02;

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
            SwapMusicTrack(menuMusic);
        }

        public void PlaySfxAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
        {
            if (CurrentSoundMode is SoundModes.SfxOnly or SoundModes.MusicPlusSfx)
                AudioSource.PlayClipAtPoint(clip, position, volume);
        }

        public void PlaySfxSimple(AudioClip clip, float volume = 1f)
        {
            if (CurrentSoundMode is SoundModes.SfxOnly or SoundModes.MusicPlusSfx)
                sfxSource.PlayOneShot(clip, volume);
        }

        public void SwapMusicTrack(AudioClip music)
        {
            StopAllCoroutines();
            StartCoroutine(FadeMusicTracks(music));
        }

        private IEnumerator FadeMusicTracks(AudioClip newMusicTrack)
        {
            const float timeToFade = 2f;
            var timeElapsed = 0f;

            var fromAudionSource = musicSource01.isPlaying ? musicSource01 : musicSource02;
            var toAudioSource = musicSource01.isPlaying ? musicSource02 : musicSource01;

            toAudioSource.clip = newMusicTrack;
            toAudioSource.Play();

            while (timeElapsed < timeToFade)
            {
                toAudioSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                fromAudionSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            fromAudionSource.Stop();
        }

        public void SetSoundMode(int soundMode)
        {
            CurrentSoundMode = (SoundModes) soundMode;
            sfxSource.mute = CurrentSoundMode is SoundModes.None or SoundModes.MusicOnly;
            musicSource01.mute = CurrentSoundMode is SoundModes.None or SoundModes.SfxOnly;
            musicSource02.mute = CurrentSoundMode is SoundModes.None or SoundModes.SfxOnly;
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