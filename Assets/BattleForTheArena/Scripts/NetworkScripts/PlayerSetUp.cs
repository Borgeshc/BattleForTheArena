using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetUp : NetworkBehaviour
{
    public Behaviour[] componentsToDisable;

    private void Start()
    {
        if(!isLocalPlayer)                                  //If we are not controlling thr player
        {
            foreach(Behaviour b in componentsToDisable)     //Disable all of the components
            {
                b.enabled = false;
            }
        }

        RegisterPlayer();
    }

    void RegisterPlayer()   //Changes the name of the player to "Player " + their Network ID number
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }
}
