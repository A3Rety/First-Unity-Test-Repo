using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float _damageCooldown = 1.0f;
    private float _lastDamageTime = -Mathf.Infinity;

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _pounchPower = 10.0f;


    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            if (Time.time - _lastDamageTime > _damageCooldown && player != null)
            {
                player.gameObject.GetComponent<PlayerLive>()?.TakeDamage(_damage);

                PlayerPounch(player.gameObject);

                _lastDamageTime = Time.time;
            }
        }
    }

    private void PlayerPounch(GameObject obj)
    {
        Rigidbody player = obj.GetComponent<Rigidbody>();

        Vector3 direction = (obj.transform.position - transform.position).normalized;

        player.AddForce(direction * _pounchPower, ForceMode.Impulse);

        GetComponent<Rigidbody>().AddForce(-direction * _pounchPower / 3f, ForceMode.Impulse);
    }
}
