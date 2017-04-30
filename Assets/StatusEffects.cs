using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public  bool GotBurning;
    public  bool GotSlowed;
    public  bool GotStunned;
    public  bool GotKnockedDown;
    public  bool GotRooted;
    public  bool GotFrozen;
    public  bool GotBleeding;

    // Everything above is for testing

    public static bool statusEffectActive;
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

    private void Update()
    {
        if(!statusEffectActive)
        {
            if (GotBurning)
            {
                isBurning = true;
                ApplyEffect(5, 0);
            }
            else if (GotSlowed)
            {
                isSlowed = true;
                ApplyEffect(0, 5);
            }
            else if (GotStunned)
            {
                isStunned = true;
                ApplyEffect(0, 0);
            }
            else if (GotKnockedDown)
            {
                isKnockedDown = true;
                ApplyEffect(0, 0);
            }
            else if (GotRooted)
            {
                isRooted = true;
                ApplyEffect(0, 0);
            }
            else if (GotFrozen)
            {
                isFrozen = true;
                ApplyEffect(0, 0);
            }
            else if (GotBleeding)
            {
                isBleeding = true;
                ApplyEffect(4, 0);
            }
        }
  
    }

    public void StopCoroutines()
    {
        if(stunned != null)
        StopCoroutine(stunned);

        if(knockedDown != null)
        StopCoroutine(knockedDown);

        if(rooted != null)
        StopCoroutine(rooted);

        if(frozen != null)
        StopCoroutine(frozen);

        anim.SetBool("Stunned", false);
        anim.SetBool("KnockedDown", false);
        Movement.unableToAct = false;
        statusEffectActive = false;
        isStunned = false;
        isKnockedDown = false;
        isRooted = false;
        isFrozen = false;

        //These four are for testing
        GotStunned = false;
        GotKnockedDown = false;
        GotRooted = false;
        GotFrozen = false;

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
            statusEffectActive = true;
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
            statusEffectActive = true;
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
            statusEffectActive = true;
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
            statusEffectActive = true;
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
        isBurning = false;

        statusEffectActive = false;
        GotBurning = false;
    }

    IEnumerator Slowed(float slowAmt)
    {
        movement.speed = movement.baseSpeed / slowAmt;
        yield return new WaitForSeconds(slowedLength);
        movement.speed = movement.baseSpeed;
        isSlowed = false;

        statusEffectActive = false;
        GotSlowed = false;
    }

    IEnumerator Stunned()
    {
        Movement.unableToAct = true;
        anim.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunnedLength);
        anim.SetBool("Stunned", false);
        Movement.unableToAct = false;
        isStunned = false;

        statusEffectActive = false;
        GotStunned = false;
    }

    IEnumerator KnockedDown()
    {
        Movement.unableToAct = true;
        anim.SetBool("KnockedDown", true);
        yield return new WaitForSeconds(knockedDownLength);
        anim.SetBool("KnockedDown", false);
        Movement.unableToAct = false;
        isKnockedDown = false;

        statusEffectActive = false;
        GotKnockedDown = false;
    }

    IEnumerator Rooted()
    {
        Movement.unableToAct = true;
        yield return new WaitForSeconds(rootedLength);
        Movement.unableToAct = false;
        isRooted = false;

        statusEffectActive = false;
        GotRooted = false;
    }

    IEnumerator Frozen()
    {
        Movement.unableToAct = true;
        yield return new WaitForSeconds(frozenLength);
        Movement.unableToAct = false;
        isFrozen = false;

        statusEffectActive = false;
        GotFrozen = false;
    }

    IEnumerator Bleeding(float damage)
    {
        for (int i = 0; i < bleedLength; i++)
        {
            health.TookDamage(damage);
            yield return new WaitForSeconds(1);
        }
        isBleeding = false;

        statusEffectActive = false;
        GotBleeding = false;
    }
}
