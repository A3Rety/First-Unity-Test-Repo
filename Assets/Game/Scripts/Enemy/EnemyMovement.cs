using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _animation;
    private Rigidbody _rb;

    private bool _isPlaying;

    private void Start()
    {
        GameStateManager.Singleton.OnGameEnd += StopMoving;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animation = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();

        _isPlaying = true;
        _animation.SetBool("isFollowing", true);
    }

    private void Update()
    {
        if (_player == null | _animation == null)
        {
            Debug.Log("null");
            return;
        }

        if (_isPlaying)
            MoveToObject(_player.position);
    }

    private void MoveToObject(Vector3 target)
    {
        _agent.SetDestination(target);
    }

    private void StopMoving()
    {
        _isPlaying = false;
        _animation.SetBool("isFollowing", false);

        _rb.constraints = RigidbodyConstraints.None;
        _agent.isStopped = true;
        _agent.ResetPath();
        _agent.velocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        GameStateManager.Singleton.OnGameEnd -= StopMoving;
    }
}
