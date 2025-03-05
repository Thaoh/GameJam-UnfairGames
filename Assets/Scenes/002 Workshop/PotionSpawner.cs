using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PotionSpawner : MonoBehaviour {
	[SerializeField] private Sprite[] _potionSprites;
	[SerializeField] private GameObject _potionPrefab;
	[SerializeField] private int _potionBase = 100;
	[SerializeField] private RangedFloat _potionMultiplier = new(0.4f, 0.7f);
	[SerializeField] private bool _randomPositionInBounds = false;
	[SerializeField] private Transform _itemParent;
	private Bounds _bounds;

	private void Awake() {
		var collider = GetComponent<Collider2D>();
		_bounds = new Bounds(transform.position, collider.bounds.extents);
		var potionsToSpawn = (int)(_potionBase * Random.Range(_potionMultiplier.minValue, _potionMultiplier.maxValue));

		for (var i = 0; i < potionsToSpawn; i++) {
			SpawnItem(i);
		}
	}

	private void SpawnItem(int potionIndex) {
		var sprite = _potionSprites[Random.Range(0, _potionSprites.Length)];
		var newSpawn = Instantiate(_potionPrefab, GetRandomPointInside(_bounds, potionIndex, 0.7f), Quaternion.identity);

		if (newSpawn.TryGetComponent(out Potion potion)) {
			potion.Sprite.sprite = sprite;
			potion.Sprite.transform.localScale = new Vector3(
				Random.Range(0.025f, 0.075f), 
				Random.Range(0.025f, 0.075f), 
				Random.Range(0.025f, 0.075f)
			);
			potion.Sprite.color = Random.ColorHSV(0.3f, 1f, 0.3f, 1f, 1f, 1f);

			potion.Sprite.sortingOrder = 2 + potionIndex;
		}

		if (_itemParent != null) {
			newSpawn.transform.SetParent(_itemParent);
		}
	}

	public Vector3 GetRandomPointInside(Bounds bounds, int potionIndex, float increment = 1f) {
		var size = bounds.size;
		var center = bounds.center;
		Vector3 point;
		
		float x = center.x - size.x / 2f + bounds.extents.x;
		if (_randomPositionInBounds) {
			point.x = center.x + Random.Range(-size.x, size.x);
		} else {
			point.x = (center.x - size.x) + (potionIndex * increment);
		}
		
		point.y = center.y + Random.Range(-size.y / 2.2f, size.y / 2.2f);
		point.z = transform.position.z;
		
		return point;
	}
}