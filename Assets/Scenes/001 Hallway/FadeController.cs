using System;
using UnityEngine;

public class FadeController : MonoBehaviour {
    [SerializeField] private AnimationCurve _fadeCurve;
    [SerializeField] private SpriteRenderer _fadeOverlay;

    private float _fadeMaxByTime = Mathf.Infinity;
    private float _fadeDelay;
    private bool _isRendering = false;
    public static Action OnFadeComplete { get; set; } = delegate { };

    private void Update() {
        FadeByTime();
    }

    private void FadeByTime() {
        if (!_isRendering) {
            
            return;
        }
        
        if (Time.realtimeSinceStartup <= _fadeMaxByTime ) {
            float fadeStart = _fadeMaxByTime - _fadeDelay;
            float fadeRatio = (Time.realtimeSinceStartup - fadeStart) / _fadeDelay;

            float alpha = _fadeCurve.Evaluate(fadeRatio);
            SetAlpha(alpha);
        } else {
            SetAlpha(1f);
            OnFadeComplete?.Invoke();
        }
    }

    private void SetAlpha( float alpha ) {
        _fadeOverlay.color = new Color(0f, 0f, 0f, alpha);
    }

    public void StartFade(float fadebyTime, float fadeDelay = 2f) {
        _fadeMaxByTime = fadebyTime;
        _fadeDelay = fadeDelay;
        _isRendering = true;
    }
}
