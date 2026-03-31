using UnityEngine;
using UnityEngine.AI;

using UnityEngine;
using UnityEngine.AI;
public class AgentSimpleController : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            agent.SetDestination(Target.position);
        }
    }

    // Detecta la colisión con el Target
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Target)
        {
            Destroy(gameObject); // Destruye el agente
        }
    }
}
