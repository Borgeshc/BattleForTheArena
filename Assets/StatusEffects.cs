using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusEffects : MonoBehaviour
{
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

    Image burnImg;
    Image slowedImg;
    Image stunnedImg;
    Image knockedDownImg;
    Image rootedImg;
    Image frozenImg;
    Image bleedingImg;

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
        burnImg = GameObject.Find("Burning").GetComponent<Image>();
        slowedImg = GameObject.Find("Slowed").GetComponent<Image>();
        stunnedImg = GameObject.Find("Stunned").GetComponent<Image>();
        knockedDownImg = GameObject.Find("KnockedDown").GetComponent<Image>();
        rootedImg = GameObject.Find("Rooted").GetComponent<Image>();
        frozenImg = GameObject.Find("Frozen").GetComponent<Image>();
        bleedingImg = GameObject.Find("Bleeding").GetComponent<Image>();

        burnImg.gameObject.SetActive(false);
        slowedImg.gameObject.SetActive(false);
        stunnedImg.gameObject.SetActive(false);
        knockedDownImg.gameObject.SetActive(false);
        rootedImg.gameObject.SetActive(false);
        frozenImg.gameObject.SetActive(false);
        bleedingImg.gameObject.SetActive(false);
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
        isStunned = false;
        isKnockedDown = false;
        isRooted = false;
        isFrozen = false;

        stunnedImg.fillAmount = 1;
        knockedDownImg.fillAmount = 1;
        rootedImg.fillAmount = 1;
        frozenImg.fillAmount = 1;

        stunnedImg.gameObject.SetActive(false);
        knockedDownImg.gameObject.SetActive(false);
        rootedImg.gameObject.SetActive(false);
        frozenImg.gameObject.SetActive(false);
    }

    IEnumerator UpdateEffectImg(Image image, float length)
    {
        if(image.IsActive())
        {
            for (int i = 0; i < length; i++)
            {
                if (image.IsActive())
                {
                    image.fillAmount -= (1 / length);
                    yield return new WaitForSeconds(1);
                }
                else
                    break;
            }
            image.fillAmount = 1;
        }
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
        burnImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(burnImg, burnLength));

        for(int i = 0; i < burnLength; i++)
        {
            health.TookDamage(damage);
            yield return new WaitForSeconds(1);
        }
        isBurning = false;
        burnImg.gameObject.SetActive(false);
    }

    IEnumerator Slowed(float slowAmt)
    {
        slowedImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(slowedImg, slowedLength));

        movement.speed = movement.baseSpeed / slowAmt;
        yield return new WaitForSeconds(slowedLength);
        movement.speed = movement.baseSpeed;
        isSlowed = false;
        slowedImg.gameObject.SetActive(false);
    }

    IEnumerator Stunned()
    {
        stunnedImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(stunnedImg, stunnedLength));

        Movement.unableToAct = true;
        anim.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunnedLength);
        anim.SetBool("Stunned", false);
        Movement.unableToAct = false;
        isStunned = false;
        stunnedImg.gameObject.SetActive(false);
    }

    IEnumerator KnockedDown()
    {
        knockedDownImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(knockedDownImg, knockedDownLength));

        Movement.unableToAct = true;
        anim.SetBool("KnockedDown", true);
        yield return new WaitForSeconds(knockedDownLength);
        anim.SetBool("KnockedDown", false);
        Movement.unableToAct = false;
        isKnockedDown = false;
        knockedDownImg.gameObject.SetActive(false);
    }

    IEnumerator Rooted()
    {
        rootedImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(rootedImg, rootedLength));

        Movement.unableToAct = true;
        yield return new WaitForSeconds(rootedLength);
        Movement.unableToAct = false;
        isRooted = false;
        rootedImg.gameObject.SetActive(false);
    }

    IEnumerator Frozen()
    {
        frozenImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(frozenImg, frozenLength));

        Movement.unableToAct = true;
        yield return new WaitForSeconds(frozenLength);
        Movement.unableToAct = false;
        isFrozen = false;
        frozenImg.gameObject.SetActive(false);
    }

    IEnumerator Bleeding(float damage)
    {
        bleedingImg.gameObject.SetActive(true);
        StartCoroutine(UpdateEffectImg(bleedingImg, bleedLength));

        for (int i = 0; i < bleedLength; i++)
        {
            health.TookDamage(damage);
            yield return new WaitForSeconds(1);
        }
        isBleeding = false;
        bleedingImg.gameObject.SetActive(false);
    }
}
