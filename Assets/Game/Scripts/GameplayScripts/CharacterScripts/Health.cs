using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float baseHealth;
    public Image healthBar;
    public float deathAnimLength;
    public float hitAnimLength;

    bool hitEffectActive;
    bool isDead;
    Animator anim;
    float health;

    private void Start()
    {
        health = baseHealth;
        anim = GetComponent<Animator>();
    }

    public void TookDamage(float damage)
    {
        if (!Block.isBlocking)
        {
            health -= damage;
            healthBar.fillAmount = health / baseHealth;

            if(!hitEffectActive)
            {
                hitEffectActive = true;
                StartCoroutine(HitEffect());
            }

            if (health <= 0 && !isDead)
            {
                isDead = true;
                StartCoroutine(Died());
            }
        }
    }

    IEnumerator HitEffect()
    {
        //Animation HitEffect based on direction hit

        yield return new WaitForSeconds(hitAnimLength);
        hitEffectActive = false;
    }

    IEnumerator Died()
    {
        Movement.canMove = false;

        //Change to animation Death based on direction hit
        anim.SetBool("Died", true);
        yield return new WaitForSeconds(deathAnimLength);
        Destroy(gameObject);
    }
}
