using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private Transform nextLevelSpawnPoint;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var playerMovement = col.GetComponent<PlayerMovement>();
        if (playerMovement)
        {
            StartCoroutine(DelayBeforeLevelLoad());
        }
    }

    private IEnumerator DelayBeforeLevelLoad()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level 2");
    }
}
