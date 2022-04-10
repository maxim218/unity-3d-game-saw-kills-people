using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour {
    private const float WaitingFloat = 1.2f;
    
    private IEnumerator KillMe() {
        yield return new WaitForSeconds(WaitingFloat);
        Destroy(gameObject);
    }
    
    private void Start() {
        bool contains = gameObject.name.Contains("HeroDeathPrefab");
        if (contains) return;
        StartCoroutine( KillMe() );
    }
}
