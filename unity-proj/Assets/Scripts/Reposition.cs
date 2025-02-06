using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area") == false)
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
        
        Vector3 playerDirection = GameManager.Instance.player.inputVector2;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case GameConstants.Tags.Ground:
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * directionX * GameConstants.DoubleMapSize);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * directionY * GameConstants.DoubleMapSize);
                }
                break;
            case GameConstants.Tags.Enemy:
                if (collider2D.enabled)
                {
                    transform.Translate(playerDirection * GameConstants.MapSize +
                                        new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
