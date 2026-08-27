using UnityEngine;
using TMPro;

public class PlayerLive : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _wallBoundarySound;
    [SerializeField] private AudioClip _pickUpSound;

    [SerializeField] private int _playerLives = 3;

    [SerializeField] private ParticleSystem _damageParticle;
    [SerializeField] private ParticleSystem _loseParticle;

    private TextMeshProUGUI _textLives;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _textLives = GameObject.Find("Text_Lives").GetComponent<TextMeshProUGUI>();
        _textLives.text = $"Lives: {_playerLives}";
    }

    public void TakeDamage(int damage)
    {
        _playerLives -= damage;
        _textLives.text = $"Lives: {_playerLives}";

        _audioSource.PlayOneShot(_damageSound);
        Instantiate(_damageParticle, transform.position, Quaternion.identity).Play();

        if (_playerLives <= 0)
        {
            Instantiate(_loseParticle, transform.position, Quaternion.identity).Play();
            
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<SphereCollider>().isTrigger = true;

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
