using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public List<Player> playersInScene;

    public List<Player> finishedPlayers;

    public UnityEvent giantWinsEvent;
    public UnityEvent villagersWinEvent;

    public Transform[] transformPoints;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
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
        {
            if (!finishedPlayers.Contains(targetPlayer))
                finishedPlayers.Add(targetPlayer);
        }

        targetPlayer.gameObject.SetActive(false);
        if (playersInScene.Count <= 0)
            EndGame();

    }

   

    

    public void EndGame()
    {
        if (finishedPlayers.Count == 0)
        {
            giantWinsEvent.Invoke();
            Debug.Log("Gano el Shrek");
        }
        else
        {
            villagersWinEvent.Invoke();
            Debug.Log("Los jugadores que ganaron son");
            for (int i = 0; i < finishedPlayers.Count; i++)
            {
                finishedPlayers[i].transform.position = transformPoints[i].position;
                finishedPlayers[i].gameObject.SetActive(true);
                finishedPlayers[i].WinPlayer();
                Debug.Log( (i+1).ToString() + " lugar: " + finishedPlayers[i].name);
            }
        }
    }
	
	
}
