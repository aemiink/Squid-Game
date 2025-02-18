using UnityEngine;

public class GamerController : MonoBehaviour
{
    [Header("Variables of DataTypes")]
    public float movementSpeed = 5f;  // Yava� y�r�y�� h�z�
    public float shiftSpeed = 10f;    // Ko�ma h�z�
    public float jumpForce = 7f;      // Z�plama g�c�
    private float currentSpeed;
    private float stamina;
    private bool isGrounded;

    [Header("Variables of Objects")]
    public Animator anim;
    public AudioSource foot;           // Ayak seslerini �alacak AudioSource
    public AudioClip mermer, toprak, runSound;  // Mermer, toprak, ko�ma sesleri
    private Rigidbody rb;
    private Vector3 direction;

    private string currentGroundType;  // Zemin t�r�n� belirlemek i�in de�i�ken

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        // Hareket y�n� ve h�z hesaplamas�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        float speed = direction.magnitude * currentSpeed;
        anim.SetFloat("Speed", speed);

        // Stamina i�leyi�i
        if (stamina > 5f) stamina = 5f;
        else if (stamina < 0) stamina = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;  // Ko�ma h�z�
            }
            else
            {
                currentSpeed = movementSpeed;  // Yava� y�r�y��
            }
        }
        else
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;  // Yava� y�r�y��
        }

        // Y�r�y�� ve ko�ma sesini �almak i�in gerekli kontrol
        if (isGrounded && direction.magnitude > 0.1f) // Hareket varsa
        {
            PlayFootstepSound();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;

            // Zemin t�r�n� belirlemek i�in collider'�n layer'�na g�re ses se�imi
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Mermer"))
            {
                currentGroundType = "Mermer";
            }
            else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Toprak"))
            {
                currentGroundType = "Toprak";
            }
            else
            {
                currentGroundType = "Di�er"; // Di�er zemin t�r�
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            foot.Stop(); // Zemin de�i�ince ses durdurulmal�
        }
    }

    // Zemin t�r�ne g�re ayak sesini �al
    private void PlayFootstepSound()
    {
        if (currentGroundType == "Mermer")
        {
            // Mermer zemini i�in mermer sesi �al
            if (!foot.isPlaying) // Ses zaten �alm�yorsa
                foot.PlayOneShot(mermer);
        }
        else if (currentGroundType == "Toprak")
        {
            // Toprak zemini i�in toprak sesi �al
            if (!foot.isPlaying) // Ses zaten �alm�yorsa
                foot.PlayOneShot(toprak);
        }
        else
        {
            // Di�er zeminde herhangi bir ses �almak isterseniz, buraya ekleme yapabilirsiniz.
        }

        // Ko�ma sesini �al
        if (currentSpeed == shiftSpeed && !foot.isPlaying)  // Ko�ma h�z� ise ve ses �alm�yorsa
        {
            foot.PlayOneShot(runSound); // Ko�ma sesini �al
        }
    }
}
