using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [Header("Effect variables")]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private SpriteRenderer buttonIcon;

    // Enemies variables
    private List<Player> playersInside;
    private Player _holdedPlayer;

    // Position variables
    [Header("Position variables")]
    public Vector3 offsetPosition;

    private void OnTriggerEnter(Collider other)
    {
        _animator.Play("ShakeEnter");
        Player _enteredPlayer = other.GetComponent<Player>();
        if (_enteredPlayer != null)
            playersInside.Add(_enteredPlayer);
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.Play("ShakeExit");
        Player _enteredPlayer = other.GetComponent<Player>();
        if (playersInside.Contains(_enteredPlayer))
            playersInside.Remove(_enteredPlayer);
    }

    void Start()
    {
        playersInside = new List<Player>();
    }

    void LateUpdate()
    {
        if (_holdedPlayer==null)
        {
            for (int i = 0; i < playersInside.Count; i++)
            {
                if (Input.GetButtonDown(playersInside[i].buttonString))
                {
                    _holdedPlayer = playersInside[i];
                    buttonIcon.gameObject.SetActive(false);
                    break;
                }
            }

            if (buttonIcon != null)
            {
                buttonIcon.transform.LookAt(CameraManager.instance.villagersCamera.transform);
            }
        }
        else
        {
            this.transform.position = _holdedPlayer.transform.position + 
                (_holdedPlayer.playerState.Equals(Player.EnumPlayerState.walking) ? Vector3.zero : offsetPosition);

            this.transform.localEulerAngles = _holdedPlayer.transform.eulerAngles;
            if (Input.GetButtonDown(_holdedPlayer.buttonString))
            {
                buttonIcon.gameObject.SetActive(true);
                _holdedPlayer = null;
            }

        }

    }
}
