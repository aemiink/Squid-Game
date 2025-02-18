using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool canMove = false;

    void Update()
    {
        if (canMove)
        {
            float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(0, 0, move);
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    // Hareketi kontrol etme
    public bool CanMove()
    {
        return canMove;
    }
}
