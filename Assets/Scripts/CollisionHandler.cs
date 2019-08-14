using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    [Header("Particle Effects")]
    [SerializeField] GameObject crashFX;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
        crashFX.SetActive(true);
        Invoke("ReloadScene", 3f);
    }

    void StartCrashSequence() // string referenced
    {
        SendMessage("OnPlayerCrash");
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
