using UnityEngine;

/*
 * Выстрел ракетами
 */
public class BulletFire : MonoBehaviour
{
    
    public GameObject Bullet; // снаряд
    public GameObject Target1; // точка старта снаряда
    public GameObject Target2; // точка старта снаряда
    public Animator bulletAnim;
    public GameManager GM;
    private void LateUpdate()
    {
        Fire();
    }

    /*Создаём обьекты ракет  в зависимости есть ли они в наличии*/
    void Fire()
    {
        Bullet.transform.rotation = Quaternion.Euler(0, 0, -90);
        
        if(GM.BulletCount == 0)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletAnim.Play("up");
            GM.BulletCount--;
            AudioManager.Instance.PlayRocketStartEffect();
            
            Vector3 SpawnPoint1 = Target1.transform.position;
            GameObject rocket1 = Instantiate(Bullet, SpawnPoint1, Quaternion.identity);

            Vector3 SpawnPoint2= Target2.transform.position;
            GameObject rocket2 = Instantiate(Bullet, SpawnPoint2, Quaternion.identity);
            
            Rigidbody Run1 = rocket1.GetComponent<Rigidbody>();
            Run1.AddForce(rocket1.transform.right * 7, ForceMode.Impulse);
            Destroy(rocket1, 3);
            
            Rigidbody Run2 = rocket2.GetComponent<Rigidbody>();
            Run2.AddForce(rocket2.transform.right * 7, ForceMode.Impulse);
            Destroy(rocket2, 3);
        }
    }
}
