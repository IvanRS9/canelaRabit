using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public int maximoEnemigos;
    public int numeroEnemigos = 0;
    public float rangoDeteccion = 5f;

    private List<GenericEnemy> enemigosEnLaZona = new List<GenericEnemy>();
    private bool jugadorEnRango = false;

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0, 1);
    }

    void GenerarEnemigo()
    {
        if (numeroEnemigos < maximoEnemigos)
        {
            GameObject enemigoObj = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
            numeroEnemigos++;

            // Agrega el enemigo a la lista
            GenericEnemy enemigo = enemigoObj.GetComponent<GenericEnemy>();
            if (enemigo != null)
            {
                enemigosEnLaZona.Add(enemigo);

                // Si el jugador est� en rango, activa el movimiento del enemigo
                if (jugadorEnRango)
                {
                    enemigo.ActivarMovimiento(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            NotificarEnemigos(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            NotificarEnemigos(false);
        }
    }

    void NotificarEnemigos(bool activar)
    {
        foreach (var enemigo in enemigosEnLaZona)
        {
            if (enemigo != null)
            {
                enemigo.ActivarMovimiento(activar);
            }
        }
    }
}
