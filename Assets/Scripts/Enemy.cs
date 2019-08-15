using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] GameObject parent;

    ScoreBoard scoreBoard;

    private void Awake() {
        AddBoxCollider();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other) {
        GameObject fx = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent.transform;
        Destroy(gameObject);
    }
    
    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
}
