using UnityEngine;


// Дочерки навешивать на триггеры
public class Interactable : MonoBehaviour
{
    private int countCollisions = 0;

    public virtual void OnInteract(GameObject interactor) 
    {
        OnInteract();
    }
    public virtual void OnInteract()
    {
        //Debug.Log($"Сработала интеракция");
    }

    public virtual void OnDeactivate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            if (countCollisions == 0) 
            {
                Debug.Log($"Слайм зашёл в {transform.name}");
                OnInteract(collision.gameObject);
            }

            ++countCollisions;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            --countCollisions;

            if (countCollisions <= 0)
            {
                OnDeactivate();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            if (countCollisions == 0)
            {
                Debug.Log($"Слайм коснулся {transform.name}");
                OnInteract(collision.gameObject);
            }

            ++countCollisions;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            --countCollisions;

            if (countCollisions <= 0)
            {
                OnDeactivate();
            }
        }

    }
}