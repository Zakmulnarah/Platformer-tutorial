using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //making it able to call elsewhere
    public static PlayerHealthController instance;
    //invincible
    public float invincibleLength;
    private float invincibleCounter;
    private SpriteRenderer theSR;

    public int currentHealth, maxHealth;

    public GameObject deathEffect;
    //need this to call elsewhere too
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter>0)
        {
            //deltatime if taken away from a value, it takes ONE value per ONE second!!
            invincibleCounter -= Time.deltaTime;
            //turning player back to visible after invincibility
            if (invincibleCounter<=0)
            {
                theSR.color=new Color(theSR.color.r,theSR.color.g,theSR.color.b,1f);
            }
        }
    }
    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
              //  gameObject.SetActive(false);
                Instantiate(deathEffect,transform.position,transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                //invincible colour on player
                theSR.color = new Color(theSR.color.r,theSR.color.g,theSR.color.b,0.5f);
                //calling the method from PlayerController
                AudioManager.instance.PlaySFX(9);
                PlayerController.instance.Knockback();

            }
            UIController.instance.UpdateHealthDisplay();
        }
    }
    public void HealPlayer()
    {
        PlayerHealthController.instance.currentHealth++;
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}

