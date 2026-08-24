using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int _rewardScore = 10;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Singleton.AddScore(_rewardScore);
            
            gameObject.SetActive(false);
        }
    }
}
