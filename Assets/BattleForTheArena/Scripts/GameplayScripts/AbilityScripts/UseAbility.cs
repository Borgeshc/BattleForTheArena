using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : Ability
{
    public GameObject Effect;
    public GameObject spawnpoint;
    public float casttime;
    public float cooldown;

    public enum SpawnRotation
    {
        ForwardRotation,
        InvertedRotation,
        SpawnpointRotation
    };

    public SpawnRotation spawnRotation = SpawnRotation.ForwardRotation;

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

        switch(spawnRotation)
        {
            case SpawnRotation.ForwardRotation:
                Instantiate(Effect, spawnpoint.transform.position, transform.rotation);
                break;
            case SpawnRotation.InvertedRotation:
                Instantiate(Effect, spawnpoint.transform.position, new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w));
                break;
            case SpawnRotation.SpawnpointRotation:
                Instantiate(Effect, spawnpoint.transform.position, spawnpoint.transform.rotation);
                break;
        }

        anim.SetBool("BasicAttack", false);

        yield return new WaitForSeconds(cooldown);
        readyToFire = true;
    }
}
