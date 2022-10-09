using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject burningParticles;
    private SessionTimeHandler sessionTimeHandlerInstance;

    private void Start()
    {
        sessionTimeHandlerInstance = SessionTimeHandler.Instance;
    }
    public void Die()
    {
        GetComponent<InputManager>().enabled = false;
        burningParticles.SetActive(true);
        sessionTimeHandlerInstance.EndGame();
    }
}
