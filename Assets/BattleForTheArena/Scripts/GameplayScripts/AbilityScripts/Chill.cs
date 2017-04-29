using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chill : NetworkBehaviour
{

    public GameObject Effect;
    public GameObject spawnpoint;
    public float casttime;
    public float cooldown;
    public bool readyToFire;

    public enum SpawnRotation
    {
        ForwardRotation,
        InvertedRotation,
        SpawnpointRotation
    };

    public SpawnRotation spawnRotation = SpawnRotation.ForwardRotation;



    public enum KeyBind
    {
        RightTrigger,
        ActionTwo,
        ActionThree,
        ActionFour
    };

    public KeyBind keyBind = KeyBind.RightTrigger;

    Animator anim;

    void OnEnable()
    {
        switch (keyBind)
        {
            case KeyBind.RightTrigger:
                AbilityManager.OnRightTriggerPressed += FireAbility;
                break;
            case KeyBind.ActionTwo:
                AbilityManager.OnActionTwoPressed += FireAbility;
                break;
            case KeyBind.ActionThree:
                AbilityManager.OnActionThreePressed += FireAbility;
                break;
            case KeyBind.ActionFour:
                AbilityManager.OnActionFourPressed += FireAbility;
                break;
            default:
                break;
        }
    }

    void OnDisable()
    {
        switch (keyBind)
        {
            case KeyBind.RightTrigger:
                AbilityManager.OnRightTriggerPressed -= FireAbility;
                break;
            case KeyBind.ActionTwo:
                AbilityManager.OnActionTwoPressed -= FireAbility;
                break;
            case KeyBind.ActionThree:
                AbilityManager.OnActionThreePressed -= FireAbility;
                break;
            case KeyBind.ActionFour:
                AbilityManager.OnActionFourPressed -= FireAbility;
                break;
            default:
                break;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        readyToFire = true;
        CmdInitOnServer();
    }

    [Command]
    void CmdInitOnServer()
    {
        anim = GetComponent<Animator>();
        readyToFire = true;
    }

    public void FireAbility()
    {
        CmdFireAbility();
    }

    [Command]
    void CmdFireAbility()
    {
        readyToFire = false;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        anim.SetBool("BasicAttack", true);
        yield return new WaitForSeconds(casttime);

        switch (spawnRotation)
        {
            case SpawnRotation.ForwardRotation:
                GameObject clone = Instantiate(Effect, spawnpoint.transform.position, transform.rotation) as GameObject;
                NetworkServer.Spawn(clone);
                break;
            case SpawnRotation.InvertedRotation:
                GameObject clone1 = Instantiate(Effect, spawnpoint.transform.position, new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w)) as GameObject;
                NetworkServer.Spawn(clone1);
                break;
            case SpawnRotation.SpawnpointRotation:
                GameObject clone2 = Instantiate(Effect, spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
                NetworkServer.Spawn(clone2);
                break;
        }

        anim.SetBool("BasicAttack", false);

        yield return new WaitForSeconds(cooldown);
        readyToFire = true;
    }
}
