using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] GameObject parent;
    [SerializeField] int scorePerHit = 14;
    [SerializeField] int health = 60;

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
    
    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        scoreBoard.ScoreHit(scorePerHit);
        health -= GameObject.FindObjectOfType<PlayerController>().CalculateWeaponDamage();
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent.transform;
        Destroy(gameObject);
    }
}
