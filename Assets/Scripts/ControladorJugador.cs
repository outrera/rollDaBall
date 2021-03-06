﻿using UnityEngine;
using UnityEngine.UI;

public class ControladorJugador : MonoBehaviour {

    public float velocidad;
    int contador;
    int nItems;
    int alturaSalto;
    bool contactoConOtroObjeto;

    Rigidbody cuerpoRigido;
    Vector3 definirMovimiento;
    public Text puntuacion;
    public Text mensajeFinal;

    // Aparicion del objeto
    public void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        contador = 0;
        nItems = 4;
        alturaSalto = 300;
        contactoConOtroObjeto = false;
        ActualizarMarcador();

        // Desactivando el mensaje de final
        mensajeFinal.gameObject.SetActive(false);
    }

    // A cada "fotograma" que envuelva físicas
    public void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        definirMovimiento = new Vector3(movHorizontal, 0, movVertical);

        if (Input.GetKeyDown(KeyCode.Space) && contactoConOtroObjeto)
        {
            cuerpoRigido.AddForce(Vector3.up * alturaSalto);
        }
        contactoConOtroObjeto = false;
        cuerpoRigido.AddForce(definirMovimiento * velocidad);
    }

    void OnCollisionStay()
    {
        contactoConOtroObjeto = true;
    }

    // Cada vez que entre en un Collider IsTriggered
    public void OnTriggerEnter(Collider objetoEnColision)
    {
        Destroy(objetoEnColision.gameObject);
        contador++;
        ActualizarMarcador();

        // Cuando tenemos todos los items, se muestra el mensaje de final
        if (contador == nItems)
            mensajeFinal.gameObject.SetActive(true);
    }

    // Actualizando marcador a cada recogida de item y Awake() de jugador
    private void ActualizarMarcador()
    {
        puntuacion.text = "Marcador: " + contador;
    }
}
