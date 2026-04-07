using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomb;
    public Transform spawnPoint;
    public float force;

    private InputSystem_Actions inputs;

    public void Awake()
    {
        // Inicializa el sistema de entrada
        inputs = new InputSystem_Actions();
    }
    public void Start()
    {
        // Asigna el evento de disparo al input "Fire"
        inputs.Player.Fire.performed += ctx => Fire();
    }
    public void OnEnable()
    {
        // Habilita el sistema de entrada
        inputs.Enable();
    }

    public void OnDisable()
    {
        // Deshabilita el sistema de entrada para evitar errores
        inputs.Disable();
    }

    public void Fire()
    {
        // Instancia la bala y aplica la fuerza
        GameObject _go = Instantiate(bomb, spawnPoint.position, Quaternion.identity);
        Rigidbody rigidbody = _go.GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Punto de inicio del rayo (posición del objeto)
        Vector3 start = transform.position;

        // Dirección del rayo (hacia adelante desde el objeto)
        Vector3 direction = transform.forward.normalized;

        // Dibujar el rayo desde la posición del objeto hacia adelante
        Gizmos.DrawRay(start, direction * 5f); // El 5f es la longitud del rayo
    }
}
