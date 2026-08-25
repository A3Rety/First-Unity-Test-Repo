using UnityEngine;

public class PlayerLive : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _wallBoundarySound;
    [SerializeField] private AudioClip _pickUpSound;

    [SerializeField] private int _playerLives = 3;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        _playerLives -= damage;
        _audioSource.PlayOneShot(_damageSound);

        if (_playerLives <= 0)
        {
            GameStateManager.Singleton.LoseGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _audioSource.PlayOneShot(_wallBoundarySound);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("PickUp"))
        {
            _audioSource.PlayOneShot(_pickUpSound);
        }
    }
}
