using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] GameObject parent;
    [SerializeField] int scorePerHit = 14;

    ScoreBoard scoreBoard;

    private void Awake() {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other) {
        scoreBoard.ScoreHit(scorePerHit);
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
