using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public List<Player> playersInScene;

    private List<Player> finishedPlayers;

    public UnityEvent giantWinsEvent;
    public UnityEvent villagersWinEvent;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        finishedPlayers = new List<Player>();
        Player[] currentPlayers = FindObjectsOfType<Player>();
        for (int i = 0; i < currentPlayers.Length; i++)
        {
            playersInScene.Add(currentPlayers[i]);
        }   
    }



    public void RemovePlayer(Player targetPlayer, bool died)
    {
        playersInScene.Remove(targetPlayer);

        if (!died)
            finishedPlayers.Add(targetPlayer);

        if (playersInScene.Count <= 0)
            EndGame();

    }

    

    public void EndGame()
    {
        if (finishedPlayers.Count == 0)
            Debug.Log("Gano el Shrek");
        else
        {
            Debug.Log("Los jugadores que ganaron son");
            for (int i = 0; i < finishedPlayers.Count; i++)
            {
                Debug.Log( (i+1).ToString() + " lugar: " + finishedPlayers[i].name);
            }
        }
    }
	
	
}
