using UnityEngine;

public class Potion : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer Sprite {get {return _spriteRenderer;}}
}
