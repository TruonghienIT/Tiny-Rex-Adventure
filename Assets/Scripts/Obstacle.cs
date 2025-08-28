using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float leftBoundary = -10f;
    
    void Update()
    {
        MoveObstacle();
    }
    private void MoveObstacle()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;
        if(transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
