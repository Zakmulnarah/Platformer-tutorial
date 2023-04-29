using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isGem;

    public bool isHeal;

    private bool isCollected;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;
                UIController.instance.UpdateGemCount();
                //to not trigger twice (bool), if multiple colliders on player
                isCollected = true;
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(6);
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
            if (isHeal)
            {
                if (PlayerHealthController.instance.maxHealth != PlayerHealthController.instance.currentHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);
                    AudioManager.instance.PlaySFX(7);
                    Instantiate(pickupEffect, transform.position,transform.rotation);
                }
            }
        }
    }
}
