using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Player playerReached = other.GetComponent<Player>();
        if (playerReached != null)
            GameManager.instance.RemovePlayer(playerReached, false);
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
