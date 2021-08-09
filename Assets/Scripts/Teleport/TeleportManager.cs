using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public Teleport[] teleports;
    // Start is called before the first frame update
    void Start()
    {
        teleports = GetComponentsInChildren<Teleport>();
        setOutTeleports();
    }

   
    private void setOutTeleports()
    {
        for (int i = 0; i <= teleports.Length-2; i+=2)
        {
            teleports[i].outTeleport=teleports[i+1];
            teleports[i + 1].outTeleport = teleports[i];
        }
    }
    
}
