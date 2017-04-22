//	EffectsPool.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class EffectsPool : MonoBehaviour {

	public const int NUM_EFFECTS = 1;
	public enum Effect : int {
		BallExplosion = 0
	}//	End public Effect enum

	public const int POOL_SIZE_PER_EFFECT = 10;

	private GameObject[,] effects;
	private int[] poolIndices;

	//	Unity Methods

	private void Awake() {
		effects = new GameObject[NUM_EFFECTS, POOL_SIZE_PER_EFFECT];
		poolIndices = new int[NUM_EFFECTS];
		for (int i = 0; i < NUM_EFFECTS; ++i) {
			poolIndices[i] = 0;
		}
		int currentEffect = (int)Effect.BallExplosion;
		GameObject ballExplosionPrefab = (GameObject)Resources.Load("Prefabs/Effects/BallExplosion");
		for (int i = 0; i < POOL_SIZE_PER_EFFECT; ++i) {
			effects[currentEffect, i] = GameObject.Instantiate(ballExplosionPrefab, transform);
			effects[currentEffect, i].SetActive(false);
		}
	}//	End Unity method Awake

	//	Additional Methods

	private GameObject nextInPool(Effect myEffect) {
		int currentEffect = (int)myEffect;
		int attempts = 0;
		while (attempts < POOL_SIZE_PER_EFFECT && effects[currentEffect, poolIndices[currentEffect]].gameObject.activeSelf) {
			++attempts;
			++poolIndices[currentEffect];
			if (poolIndices[currentEffect] >= POOL_SIZE_PER_EFFECT) { poolIndices[currentEffect] = 0; }
		}
		if (attempts >= POOL_SIZE_PER_EFFECT) {
			return null;
		}
		return effects[currentEffect, poolIndices[currentEffect]];
	}//	End private method nextInPool

	public void playEffect(Effect myEffect, Vector3 effectPosition) {
		GameObject currentEffect = nextInPool(myEffect);
		if (currentEffect != null) {
			currentEffect.transform.position = effectPosition;
			currentEffect.SetActive(true);
		}
	}//	End public method playEffect

}//	End public class EffectsPool
