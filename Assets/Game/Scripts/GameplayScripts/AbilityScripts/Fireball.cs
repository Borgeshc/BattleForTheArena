using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    public GameObject fireballEffect;
    public GameObject fireballSpawn;
    public float casttime;
    public float cooldown;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        readyToFire = true;
    }

    public override void FireAbility()
    {
        readyToFire = false;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        anim.SetBool("BasicAttack", true);
        yield return new WaitForSeconds(casttime);

        Instantiate(fireballEffect, fireballSpawn.transform.position, transform.rotation);
        anim.SetBool("BasicAttack", false);

        yield return new WaitForSeconds(cooldown);
        readyToFire = true;
    }
}
