using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour {

    public Animator anim_menu;

    public UnityEvent eventAtStart;
    public string ESTADO="";

	// Use this for initialization
	void Start () {}


    IEnumerator corrutina_SeccionTitulo() {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey) {
                anim_menu.Play("Menu_Ir_A_Instrucciones");
                break;
            }
        }

    }

    IEnumerator corrutina_SeccionInstrucciones() {
        while (true){
            yield return new WaitForEndOfFrame();
            if (Input.anyKey){
                anim_menu.Play("Menu_Ir_A_Lobby");
                break;
            }
        }
    }


    IEnumerator corrutina_SeccionLobby(){
        while (true){
            yield return new WaitForEndOfFrame();
            if (Input.anyKey) {
                anim_menu.Play("Menu_Ir_A_Comenzar");
                break;
            }
        }
    }


    IEnumerator corrutina_SeccionComenzar() {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (Input.anyKey){
                anim_menu.Play("Menu_Ir_A_Juego");
                break;
            }
        }
    }

    IEnumerator corrutina_IrAJuego() {
        while (true){
            yield return new WaitForEndOfFrame();
            if (Input.anyKey) {
                anim_menu.Play("Menu_Ir_A_Juego");
                break;
            }
        }

    }


    public void StartGame()
    {
        eventAtStart.Invoke();
    }















    /*
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown){
            Debug.Log("Se ha presionado cualquier botón.");
            CualquierTecla_Activa();
        }

    }


    public void cambiarEstado(string nuevoEstado) {
        Debug.Log("Se va a cambair estado = " + nuevoEstado);
        ESTADO = nuevoEstado;
    }


    void CualquierTecla_Activa() {

        switch (ESTADO) {
            case "":
            break;

            case "TITULO":
                Debug.Log("Caso titulo encontrado");
                irAInstrucciones();
            break;

            default:
            break;
        }


    }



    void irAInstrucciones() {
        
    }


    */
}
