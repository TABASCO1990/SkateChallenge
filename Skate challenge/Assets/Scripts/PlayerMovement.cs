using System.Collections;
using UnityEngine;

/*
 * Управление персонажем на сцене с вызовом анимаций и способностей
 */

public class PlayerMovement : MonoBehaviour
{
    public Animator SkinAnimator;
    public Animator coin;
    public Animator bulletAnim;

    private int laneNumber = 1; // номер линии
    private int lanesCount = 2; // кол-во линий

    public float firstLanePos; // позиция нулевой линии
    public float laneDistance; // растояние между линиями
    public float SideSpeed; // скорость смещения между линиями
    public float jumpSpeed = 12; //прыжок
    
    private bool isRolling; 
    public bool wannaJump; // прыжок
    
    private Vector3 ccCenterNorm = new Vector3(0,0.88f,0); // текщий размер колайдера
    private Vector3 ccCenterRoll = new Vector3(0,0.55f,0);  // колайдер в присяде
    private float ccHeightNorm = 1.77f;
    private float ccHeightRoll = 1.1f;

    private CapsuleCollider selfCollider;
    public ParticleSystem hit;
    public ParticleSystem coinTake;
    public GameManager GM;
    private Rigidbody rb;
    
    private void Start()
    {
        selfCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        SkinAnimator.SetTrigger("respawn");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0,Physics.gravity.y * 3,0),ForceMode.Acceleration); // гравитация 
        
        //прыжок
        if (wannaJump && isGrounded())
        {
            SkinAnimator.SetTrigger("jumping");
            rb.AddForce(new Vector3(0,jumpSpeed,0),ForceMode.Impulse);
            wannaJump = false;
        }

        //Чтобы эффект(ы) взрыва не двигался(ись) вместе с персонажем - задаём ему(им) вектор на правления
        GameObject[] explosion = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (var boom in explosion)
        {
            boom.transform.Translate(Vector3.left*1 * Time.deltaTime * 8);
        }
    }

    void Update()
    {
        if (isGrounded())
        {
            SkinAnimator.ResetTrigger("falling");
            if (GM.CanPlay)
            {
                if (!isRolling)
                {
                    //Прыгаем от условия
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        wannaJump = true;
                    }
                }
            }
        }
        //приземление с прыжка
        else if(rb.velocity.y < -2)
            SkinAnimator.SetTrigger("falling");
        
        Crouch(); // приседаем 
        UnCrouch(); // встаём из приседи
        
        //перемещения влево вправо между линиями
        CheckInput();
        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, firstLanePos + (laneNumber * laneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }
    
    void CheckInput()
    {
        int sign = 0;
        if(!GM.CanPlay)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            sign = -1;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            sign = 1;
        else
            return;
        
        laneNumber += sign;
        laneNumber = Mathf.Clamp (laneNumber, 0, lanesCount);
    }

    // Приседаем
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isRolling = true;
            SkinAnimator.SetBool("rolling", true);
            selfCollider.center = ccCenterRoll;
            selfCollider.height = ccHeightRoll;
        }
    }
    
    // Встаём из присяди
    void UnCrouch()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            isRolling = false;
            SkinAnimator.SetBool("rolling",false);
            selfCollider.center = ccCenterNorm;
            selfCollider.height = ccHeightNorm;
        }
    }
    
    // Столкновения с препятствиями
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Trap") || !GM.CanPlay)
            return;
        StartCoroutine(Death());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletSpawn"))
        { 
            AudioManager.Instance.RocketEffect();
            bulletAnim.Play("up");
            GM.BulletCount += 3;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            GM.AddLevel();
            GM.CanPlay = false;
            GM.ShowFinish();
        }
        if(!other.CompareTag("Coin"))
            return;
        GM.AddCoins(1);
        coin.Play("up");
        coinTake.Play();
        AudioManager.Instance.PlayCoinEffect();
        Destroy(other.gameObject);
    }

    // корутина смерти :)
    IEnumerator Death()
    {
        hit.Play();
        AudioManager.Instance.HitEffect();
        GM.CanPlay = false;
        SkinAnimator.SetTrigger("death");
        yield return new WaitForSeconds(2f);
        SkinAnimator.ResetTrigger("death");
        GM.ShowResult();
    }
    
}
