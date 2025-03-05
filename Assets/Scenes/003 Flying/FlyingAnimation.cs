using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingAnimation : MonoBehaviour
{
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private float _floatDuration = 6f; // Duration of one complete float cycle
    [SerializeField] private float _floatAmplitude = 30f; // How far the object moves up and down

    [SerializeField] private AnimationCurve _floatCurve; // Animation curve for the floating motion
    private float _timeElapsed;
    private float _originalY;
    private void Awake() {
        _originalY = _modelTransform.localPosition.y;
        _timeElapsed = Random.Range(0.4f, 6f);
    }

    /*void Start () {
        // Create a sinusoidal curve for smooth floating motion
        _floatCurve = new AnimationCurve();
        // Keyframes to approximate a smooth sine wave
        _floatCurve.AddKey( new Keyframe( 0f, 0f, Mathf.PI * 2, Mathf.PI * 2 ) ); // Start point
        _floatCurve.AddKey( new Keyframe( 0.25f, 1f, 0f, 0f ) ); // Peak
        _floatCurve.AddKey( new Keyframe( 0.5f, 0f, -Mathf.PI * 2, -Mathf.PI * 2 ) ); // Midpoint (zero crossing)
        _floatCurve.AddKey( new Keyframe( 0.75f, -1f, 0f, 0f ) ); // Trough
        _floatCurve.AddKey( new Keyframe( 1f, 0f, Mathf.PI * 2, Mathf.PI * 2 ) ); // End point
    }*/

    private void Update()
    {
        // Calculate the elapsed time and loop it based on floatDuration
        _timeElapsed += Time.deltaTime;
        var time = _timeElapsed / _floatDuration % 1f; // Normalize time to loop (0 to 1)

        // Apply the animation curve to move the RectTransform up and down
        var yOffset = _floatCurve.Evaluate(time) * _floatAmplitude;
        _modelTransform.localPosition = new Vector3(_modelTransform.position.x, _originalY+yOffset, _modelTransform.position.z);
    }
}
