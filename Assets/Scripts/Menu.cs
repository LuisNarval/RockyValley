using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{

    public Animator anim_menu;

    public UnityEvent eventAtStart;
    public string ESTADO="";

	// Use this for initialization
	void Start () {}


    IEnumerator corrutina_SeccionTitulo()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey)
            {
                anim_menu.Play("Menu_Ir_A_Instrucciones");
                break;
            }
        }

    }

    IEnumerator corrutina_SeccionInstrucciones()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey)
            {
                anim_menu.Play("Menu_Ir_A_Lobby");
                break;
            }
        }
    }


    IEnumerator corrutina_SeccionLobby()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey)
            {
                anim_menu.Play("Menu_Ir_A_Comenzar");
                break;
            }
        }
    }


    IEnumerator corrutina_SeccionComenzar()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey)
            {
                anim_menu.Play("Menu_Ir_A_Juego");
                break;
            }
        }
    }

    IEnumerator corrutina_IrAJuego()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey)
            {
                anim_menu.Play("Menu_Ir_A_Juego");
                break;
            }
        }

    }


    public void StartGame()
    {
        eventAtStart.Invoke();
    }
}
