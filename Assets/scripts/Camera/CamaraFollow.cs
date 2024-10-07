using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // La posici�n del jugador
    public Vector3 offset;    // Distancia entre la c�mara y el jugador

    void Start()
    {
        // Aseg�rate de que la c�mara est� en la posici�n correcta al inicio
        if (player == null)
        {
            Debug.LogError("La c�mara necesita un jugador para seguir. Arrastra el jugador al campo 'player'.");
            return;
        }


        //asegurar que existe un GameStateManager
        if (GameStateManager.Instance == null)
        {
            Debug.LogError("GameStateManager no est� inicializado. Aseg�rate de que el objeto est� en la escena.");
            return;
        }
        // Coloca la c�mara en la posici�n inicial deseada
        transform.position = player.position + offset;
        transform.LookAt(player);  // Hacer que la c�mara mire hacia el jugador
    }

    void LateUpdate()
    {
        // Verifica si el jugador est� asignado y GameStateManager est� inicializado
        if (player == null || GameStateManager.Instance == null) return;

        // Obtener los valores de followSpeed y rotationSpeed del GameStateManager
        float followSpeed = GameStateManager.Instance.followSpeed;
        float rotationSpeed = GameStateManager.Instance.rotationSpeed;

        // Actualiza la posici�n de la c�mara suavemente para seguir al jugador
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotar la c�mara alrededor del jugador
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
