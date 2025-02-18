using UnityEngine;

public class GamerController : MonoBehaviour
{
    [Header("Variables of DataTypes")]
    public float movementSpeed = 5f;  // Yavaþ yürüyüþ hýzý
    public float shiftSpeed = 10f;    // Koþma hýzý
    public float jumpForce = 7f;      // Zýplama gücü
    private float currentSpeed;
    private float stamina;
    private bool isGrounded;

    [Header("Variables of Objects")]
    public Animator anim;
    public AudioSource foot;           // Ayak seslerini çalacak AudioSource
    public AudioClip mermer, toprak, runSound;  // Mermer, toprak, koþma sesleri
    private Rigidbody rb;
    private Vector3 direction;

    private string currentGroundType;  // Zemin türünü belirlemek için deðiþken

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        // Hareket yönü ve hýz hesaplamasý
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        float speed = direction.magnitude * currentSpeed;
        anim.SetFloat("Speed", speed);

        // Stamina iþleyiþi
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
                currentSpeed = shiftSpeed;  // Koþma hýzý
            }
            else
            {
                currentSpeed = movementSpeed;  // Yavaþ yürüyüþ
            }
        }
        else
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;  // Yavaþ yürüyüþ
        }

        // Yürüyüþ ve koþma sesini çalmak için gerekli kontrol
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

            // Zemin türünü belirlemek için collider'ýn layer'ýna göre ses seçimi
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
                currentGroundType = "Diðer"; // Diðer zemin türü
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            foot.Stop(); // Zemin deðiþince ses durdurulmalý
        }
    }

    // Zemin türüne göre ayak sesini çal
    private void PlayFootstepSound()
    {
        if (currentGroundType == "Mermer")
        {
            // Mermer zemini için mermer sesi çal
            if (!foot.isPlaying) // Ses zaten çalmýyorsa
                foot.PlayOneShot(mermer);
        }
        else if (currentGroundType == "Toprak")
        {
            // Toprak zemini için toprak sesi çal
            if (!foot.isPlaying) // Ses zaten çalmýyorsa
                foot.PlayOneShot(toprak);
        }
        else
        {
            // Diðer zeminde herhangi bir ses çalmak isterseniz, buraya ekleme yapabilirsiniz.
        }

        // Koþma sesini çal
        if (currentSpeed == shiftSpeed && !foot.isPlaying)  // Koþma hýzý ise ve ses çalmýyorsa
        {
            foot.PlayOneShot(runSound); // Koþma sesini çal
        }
    }
}
