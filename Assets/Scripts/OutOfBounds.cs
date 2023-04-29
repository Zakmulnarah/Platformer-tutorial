using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "Player")
        //LevelManager.instance.RespawnPlayer();
        //how i did it:
        other = PlayerController.instance.GetComponent<Collider2D>();
        PlayerHealthController.instance.currentHealth = 0;
        UIController.instance.UpdateHealthDisplay();
        LevelManager.instance.RespawnPlayer();
    }
}
