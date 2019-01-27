using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [Header("Movement variables")]
    [Range(0.01f,50)]
    public float speed;
    public enum EnumPlayerControl
    {
        player_1,
        player_2,
        player_3,
        player_4
     };
    public EnumPlayerControl playerControl;
    private string horizontalAxisString;
    private string verticalAxisString;



    private CharacterController _characterController;
    private Hand _holdedHand;

    public enum EnumPlayerState
    {
        walking,
        grab,
        death
    };
    public EnumPlayerState playerState;



    private void OnTriggerEnter(Collider other)
    {
        if (playerState.Equals(EnumPlayerState.grab) && other.tag.Equals("Mouth"))
        {
            KillThisPlayer();
        }
    }

    private void Awake()
    {
        _characterController = this.GetComponent<CharacterController>();
    }

    void Start ()
    {
        // Player Axis for movement and buttons
        horizontalAxisString = "Horizontal_";
        verticalAxisString = "Vertical_";
		switch (playerControl)
        {
            case EnumPlayerControl.player_1:
                horizontalAxisString = horizontalAxisString + "P1";
                verticalAxisString = verticalAxisString + "P1";
            break;

            case EnumPlayerControl.player_2:
                horizontalAxisString = horizontalAxisString + "P2";
                verticalAxisString = verticalAxisString + "P2";
            break;

            case EnumPlayerControl.player_3:
                horizontalAxisString = horizontalAxisString + "P3";
                verticalAxisString = verticalAxisString + "P3";
            break;

            case EnumPlayerControl.player_4:
                horizontalAxisString = horizontalAxisString + "P4";
                verticalAxisString = verticalAxisString + "P4";
            break;

            default:
            break;
        }


        // Player states
        playerState = EnumPlayerState.walking;

	}
	
	void Update ()
    {
        switch(playerState)
        {
            case EnumPlayerState.walking:
                float gravity;
                Debug.Log(_characterController.isGrounded);
                gravity = _characterController.isGrounded ? 0 : -1f;

                _characterController.Move(
                    (Vector3.right * Input.GetAxis(horizontalAxisString) * speed * Time.deltaTime) +
                    (Vector3.up * gravity * Time.deltaTime) +
                    (Vector3.forward * Input.GetAxis(verticalAxisString) * speed * Time.deltaTime));

                if (this.transform.position.y < -1)
                    KillThisPlayer();
            break;

            case EnumPlayerState.death:
            break;

            case EnumPlayerState.grab:
                if (this.transform.position.y < -1)
                    KillThisPlayer();
            break;

            default:

            break;
        }
    }

    public void GrabThisPlayer()
    {
        playerState = EnumPlayerState.grab;
        _characterController.enabled = false;
        _holdedHand = GetComponentInParent<Hand>();
    }

    public void KillThisPlayer()
    {
        GameManager.instance.RemovePlayer(this, true);
        if (_holdedHand!=null)
        {
            _holdedHand.HoverUnlock(GetComponent<Interactable>());
            _holdedHand.DetachObject(this.gameObject);
        }
        
        this.gameObject.SetActive(false);
        
        
    }
}
