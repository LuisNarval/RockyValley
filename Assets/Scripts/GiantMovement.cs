using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GiantMovement : MonoBehaviour
{
    public SteamVR_Action_Boolean touchPadPressed;

    Valve.VR.InteractionSystem.Player playerVR;

    private Vector3 startingPos;
    private Vector3 startingHandPos;

    [Range(0.01f, 2f)]
    public float speed = 1;

    void Start ()
    {
        playerVR = Valve.VR.InteractionSystem.Player.instance;
        startingPos = Vector3.zero;
    }
	
	void Update ()
    {
        

        foreach (Hand hand in playerVR.hands)
        {

            if (hand.name.Contains("FallbackHand"))
                continue;

            if (touchPadPressed.GetStateDown(hand.handType))
                StartMovement(hand.transform);
            else if (touchPadPressed.GetState(hand.handType))
                CalculateMovement(hand.transform);
            else if (touchPadPressed.GetStateUp(hand.handType))
                StopMovement(hand.transform);
        }
    }

    public void StartMovement(Transform handPressed)
    {
        startingPos = this.transform.position;
        startingHandPos = handPressed.localPosition;

        Debug.Log(handPressed.name + ":" + handPressed.localPosition + " and pressed " + startingHandPos);
    }

    public void CalculateMovement(Transform handPressed)
    {
        Debug.Log(startingHandPos.x);
        Vector3 deltaMovement = handPressed.transform.localPosition - startingHandPos;
        deltaMovement *= speed;
        deltaMovement.y = 0;
        
        this.transform.position = startingPos - deltaMovement;
        
    }

    public void StopMovement(Transform handPressed)
    {

    }
}
