using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemy : MonoBehaviour
{
    public float range = 10f;
    public float moveSpeed = 3f;
    public bool InArea = false;
    Transform player;


    void Update()
    {

        coliderEnemy();
    }
    public void coliderEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        InArea = false; // Reinicio el estado de detección

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                InArea = true;
                break;
            }
        }

        if (InArea && player != null)
        {
            // muevo el enemigo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.transform == other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye el agente
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        if (InArea && player != null)
        {
            Gizmos.color = Color.red;

            Vector3 dir = player.position - transform.position;
            Gizmos.DrawRay(transform.position, dir);
        }
    }
}
