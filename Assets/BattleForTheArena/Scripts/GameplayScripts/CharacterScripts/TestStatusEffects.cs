using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatusEffects : MonoBehaviour
{
    StatusEffects statusEffects;

    private void Start()
    {
        statusEffects = GetComponent<StatusEffects>();
    }
    public void Burn()
    {
        StatusEffects.isBurning = true;
        statusEffects.ApplyEffect();
    }
    public void Slow()
    {
        StatusEffects.isSlowed = true;
        statusEffects.ApplyEffect();
    }
    public void Stun()
    {
        StatusEffects.isStunned = true;
        statusEffects.ApplyEffect();
    }
    public void KnockDown()
    {
        StatusEffects.isKnockedDown = true;
        statusEffects.ApplyEffect();
    }
    public void Root()
    {
        StatusEffects.isRooted = true;
        statusEffects.ApplyEffect();
    }
    public void Freeze()
    {
        StatusEffects.isFrozen = true;
        statusEffects.ApplyEffect();
    }
    public void Bleed()
    {
        StatusEffects.isBleeding = true;
        statusEffects.ApplyEffect();
    }
}
