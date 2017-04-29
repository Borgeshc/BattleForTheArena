using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public static bool isBurning;
    public static bool isSlowed;
    public static bool isStunned;
    public static bool isKnockedDown;
    public static bool isRooted;
    public static bool isFrozen;
    public static bool isBleeding;

    public float burnLength;
    public float slowedLength;
    public float stunnedLength;
    public float knockedDownLength;
    public float rootedLength;
    public float frozenLength;
    public float bleedLength;

    Coroutine burning;
    Coroutine slowed;
    Coroutine stunned;
    Coroutine knockedDown;
    Coroutine rooted;
    Coroutine frozen;
    Coroutine bleeding;

    Animator anim;
    Health health;
    Movement movement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
    }

    /// <summary>
    /// If does not deal damage or slows make the parameters 0
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="slowPercent">This is in percentages from 0 to 10. 0 = 0%, 5 = 50%, 10 = 100%. If you are not apply a slow effect just put this to 0.</param>
    public void ApplyEffect(float damage, float slowPercent)
    {
        if(isBurning)
        {
            if(burning != null)
            {
                StopCoroutine(burning);
                burning = StartCoroutine(Burning(damage));
            }
            else
                burning = StartCoroutine(Burning(damage));
        }
        else if(isSlowed)
        {
            if (slowed != null)
            {
                StopCoroutine(slowed);
                slowed = StartCoroutine(Slowed(slowPercent));
            }
            else
                slowed = StartCoroutine(Slowed(slowPercent));
        }
        else if (isStunned)
        {
            if (stunned != null)
            {
                StopCoroutine(stunned);
                stunned = StartCoroutine(Stunned());
            }
            else
                stunned = StartCoroutine(Stunned());
        }
        else if (isKnockedDown)
        {
            if (knockedDown != null)
            {
                StopCoroutine(knockedDown);
                knockedDown = StartCoroutine(KnockedDown());
            }
            else
                knockedDown = StartCoroutine(KnockedDown());
        }
        else if (isRooted)
        {
            if (rooted != null)
            {
                StopCoroutine(rooted);
                rooted = StartCoroutine(Rooted());
            }
            else
                rooted = StartCoroutine(Rooted());
        }
        else if (isFrozen)
        {
            if (frozen != null)
            {
                StopCoroutine(frozen);
                frozen = StartCoroutine(Frozen());
            }
            else
                frozen = StartCoroutine(Frozen());
        }
        else if (isBleeding)
        {
            if (bleeding != null)
            {
                StopCoroutine(bleeding);
                bleeding = StartCoroutine(Bleeding(damage));
            }
            else
                bleeding = StartCoroutine(Bleeding(damage));
        }
    }

    IEnumerator Burning(float damage)
    {
        for(int i = 0; i < burnLength; i++)
        {
            health.TookDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Slowed(float slowAmt)
    {
        movement.speed = movement.baseSpeed / slowAmt;
        yield return new WaitForSeconds(slowedLength);
        movement.speed = movement.baseSpeed;
    }

    IEnumerator Stunned()
    {
        movement.speed = 0;
        yield return new WaitForSeconds(stunnedLength);
        movement.speed = movement.baseSpeed;
    }

    IEnumerator KnockedDown()
    {
        movement.speed = 0;
        yield return new WaitForSeconds(knockedDownLength);
        movement.speed = movement.baseSpeed;
    }

    IEnumerator Rooted()
    {
        movement.speed = 0;
        yield return new WaitForSeconds(rootedLength);
        movement.speed = movement.baseSpeed;
    }

    IEnumerator Frozen()
    {
        movement.speed = 0;
        yield return new WaitForSeconds(frozenLength);
        movement.speed = movement.baseSpeed;
    }

    IEnumerator Bleeding(float damage)
    {
        for (int i = 0; i < bleedLength; i++)
        {
            health.TookDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }
}
