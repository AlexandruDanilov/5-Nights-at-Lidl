using UnityEngine;
using UnityEngine.Video;

namespace FiveNightsAtLidl.UI
{
    /// <summary>
    /// Prepares the menu movie, enables the player's native loop mode, and recovers
    /// if a platform video backend ever stops at the end of the clip.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VideoPlayer))]
    public sealed class LoopingMenuVideoController : MonoBehaviour
    {
        private const float RestartDelaySeconds = 0.25f;

        private VideoPlayer _videoPlayer;
        private float _stoppedAt = -1f;

        private void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _videoPlayer.isLooping = true;
        }

        private void OnEnable()
        {
            _videoPlayer.prepareCompleted += OnPrepared;
            _videoPlayer.errorReceived += OnVideoError;
            PrepareAndPlay();
        }

        private void OnDisable()
        {
            _videoPlayer.prepareCompleted -= OnPrepared;
            _videoPlayer.errorReceived -= OnVideoError;
        }

        private void Update()
        {
            if (!_videoPlayer.isPrepared)
            {
                return;
            }

            if (_videoPlayer.isPlaying)
            {
                _stoppedAt = -1f;
                return;
            }

            if (_stoppedAt < 0f)
            {
                _stoppedAt = Time.unscaledTime;
                return;
            }

            if (Time.unscaledTime - _stoppedAt >= RestartDelaySeconds)
            {
                PlayFromBeginning();
            }
        }

        private void PrepareAndPlay()
        {
            _videoPlayer.isLooping = true;
            _stoppedAt = -1f;

            if (_videoPlayer.isPrepared)
            {
                PlayFromBeginning();
                return;
            }

            _videoPlayer.Prepare();
        }

        private void OnPrepared(VideoPlayer source)
        {
            PlayFromBeginning();
        }

        private void PlayFromBeginning()
        {
            _stoppedAt = -1f;
            _videoPlayer.time = 0d;
            _videoPlayer.Play();
        }

        private static void OnVideoError(VideoPlayer source, string message)
        {
            Debug.LogError($"Menu background video failed: {message}", source);
        }
    }
}
