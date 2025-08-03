using UnityEngine;

public class Dragonfly : MonoBehaviour
{
    const float maxDistance = 100f;

    [SerializeField] Sprite flyingSprite;
    [SerializeField] float speed;

    float verticalSpeed = 0f;
    bool isFlying = false;
    Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFlying)
        {
            isFlying = true;
            playerTransform = other.transform;
            GetComponent<SpriteRenderer>().sprite = flyingSprite;
            GetComponent<AudioSource>().Play();
        }
    }

    private void Update()
    {
        if (isFlying && playerTransform != null)
        {
            Vector3 awayVector = transform.position - playerTransform.position;
            if (awayVector.magnitude > maxDistance)
            {
                Destroy(gameObject);
            }
            awayVector.y = verticalSpeed;
            transform.position += awayVector.normalized * Time.deltaTime * speed;
            verticalSpeed += Time.deltaTime * speed;
        }
    }
}
