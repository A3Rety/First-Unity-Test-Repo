using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int _rewardScore = 10;

    [SerializeField] private ParticleSystem _pickUpParticle;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Singleton.AddScore(_rewardScore);

            Instantiate(_pickUpParticle, transform.position, Quaternion.identity).Play();

            gameObject.SetActive(false);
        }
    }
}
