using UnityEngine;
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }


    private void Awake()
    {
        this.movement = GetComponent<Movement>();  
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }

    public void SetPosition(Vector3 position)
    {
        position.z = this.transform.position.z;
        this.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<GameManager>().PlayerHit();
        }
    }


    // bieganie losowo
    private void OnTriggerEnter2D(Collider2D other)
    {

        Node node = other.GetComponent<Node>();


        if (node != null && this.enabled)
        {
            // Pick a random available direction
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back the same direction so increment the index to
            // the next available direction
            if (node.availableDirections[index] == -this.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                // Wrap the index back around if overflowed
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
