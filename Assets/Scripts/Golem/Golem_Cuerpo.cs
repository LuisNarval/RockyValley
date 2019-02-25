using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Cuerpo : MonoBehaviour
{

    [Header("REFERENCIAS A ESCENA")]
    public Transform posicionCamara;

    [Header("CONFIGURACIONES")]
    public Vector3 offset;

    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        SeguirCabeza();
    }

    private void FixedUpdate(){
        SeguirCabeza();
    }

    public void SeguirCabeza(){
        this.transform.position = posicionCamara.position + offset;

        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, posicionCamara.transform.rotation.eulerAngles.y, 0.0f));
     
    }




}