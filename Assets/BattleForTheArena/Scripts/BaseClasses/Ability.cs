using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability : NetworkBehaviour
{ 
    public bool readyToFire;

    [Command]
    public virtual void CmdFireAbility()
    {

    }
}
