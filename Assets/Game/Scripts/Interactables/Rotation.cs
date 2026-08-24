using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float _speed = 75f;
    private Vector3 _angle = new Vector3(15f, 30f, 45f);


    private void Update()
    {
        transform.Rotate(_angle, _speed * Time.deltaTime);
    }
}
