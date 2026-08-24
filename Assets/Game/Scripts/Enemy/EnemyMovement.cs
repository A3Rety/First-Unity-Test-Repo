using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player == null)
        {
            Debug.Log("null");
            return;
        }

        MoveToObject(_player.position);
    }

    private void MoveToObject(Vector3 target)
    {
        _agent.SetDestination(target);
    }
}
