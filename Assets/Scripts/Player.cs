using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [Header("Movement variables")]
    [Range(0.01f, 10)]
    public float speed = 1;
    [Range(0.01f, 50)]
    public float speedRotation = 1;

    // 
    private string horizontalAxisString;
    private string verticalAxisString;
    [HideInInspector]
    public string buttonString;

    [Header("External references")]
    public Animator _animator;
    private CharacterController _characterController;
    private Hand _holdedHand;

    // Enum variables
    public enum EnumPlayerState
    {
        walking,
        grab,
        death,
        win
    };
    public EnumPlayerState playerState;

    public enum EnumPlayerControl
    {
        player_1,
        player_2,
        player_3,
        player_4
    };
    public EnumPlayerControl playerControl;


    private void OnCollisionEnter(Collision collision)
    {
        if (playerState.Equals(EnumPlayerState.grab) && this.transform.parent == null)
            KillThisPlayer();

    }

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
        buttonString = "Action_";
		switch (playerControl)
        {
            case EnumPlayerControl.player_1:
                horizontalAxisString = horizontalAxisString + "P1";
                verticalAxisString = verticalAxisString + "P1";
                buttonString = buttonString + "P1";
            break;

            case EnumPlayerControl.player_2:
                horizontalAxisString = horizontalAxisString + "P2";
                verticalAxisString = verticalAxisString + "P2";
                buttonString = buttonString + "P2";
                break;

            case EnumPlayerControl.player_3:
                horizontalAxisString = horizontalAxisString + "P3";
                verticalAxisString = verticalAxisString + "P3";
                buttonString = buttonString + "P3";
                break;

            case EnumPlayerControl.player_4:
                horizontalAxisString = horizontalAxisString + "P4";
                verticalAxisString = verticalAxisString + "P4";
                buttonString = buttonString + "P4";
                break;

            default:
            break;
        }


        // Player states
        playerState = EnumPlayerState.walking;

	}
	
	void LateUpdate ()
    {
        switch(playerState)
        {
            case EnumPlayerState.walking:
                float gravity = Physics.gravity.y;
                //gravity = _characterController.isGrounded ? 0 : Physics.gravity.y;

                Vector3 axisVector = new Vector3(Input.GetAxis(horizontalAxisString), 0, Input.GetAxis(verticalAxisString));
                this.transform.LookAt( this.transform.position + axisVector);

                _characterController.Move
                    (
                        (Vector3.up * gravity * Time.deltaTime) +
                        (this.transform.forward * axisVector.magnitude * speed * Time.deltaTime)
                    );

                _animator.SetFloat("Speed", axisVector.magnitude);

                if (this.transform.position.y < -1)
                {
                    KillThisPlayer();
                }
                    
            break;

            case EnumPlayerState.death:
            break;

            case EnumPlayerState.win:
            break;

            case EnumPlayerState.grab:
                if (this.transform.position.y < -1)
                    KillThisPlayer();
            break;

            default:

            break;
        }
    }

    public void RestoreThisPlayer()
    {
        playerState = EnumPlayerState.walking;
        _characterController.enabled = true;
        _animator.SetBool("Holding", false);
        this.transform.localEulerAngles = Vector3.zero;
    }

    public void UnattachHand()
    {
        _holdedHand = null;
    }

    public void GrabThisPlayer()
    {
        playerState = EnumPlayerState.grab;
        _characterController.enabled = false;
        _holdedHand = GetComponentInParent<Hand>();
        _animator.SetBool("Holding", true);
    }

    public void WinPlayer()
    {
        if (!playerState.Equals(EnumPlayerState.death))
            _animator.Play("Dance");
        playerState = EnumPlayerState.win;
    }

    public void KillThisPlayer()
    {
        GameManager.instance.RemovePlayer(this, true);
        /*if (_holdedHand!=null)
        {
            _holdedHand.HoverUnlock(GetComponent<Interactable>());
            _holdedHand.DetachObject(this.gameObject);
        }*/

        //this.gameObject.SetActive(false);
        //_animator.enabled = false;
        
    }
}
