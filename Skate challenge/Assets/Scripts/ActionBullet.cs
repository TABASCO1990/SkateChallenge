using UnityEngine;

/*
 * Ракета уничтожает обьекты и вызвается обьект взрыва
 */
public class ActionBullet : MonoBehaviour
{
    public GameObject explosion; //Partical Sнstem эффект взрыва
    private GameObject boom; // Обьек взрыва
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            AudioManager.Instance.PlayRocketBoomEffect();
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
        isExplosion();
    }
    
    // Создаем на сцене обьек взрыва проигрываем эффект и уничтожаем
    private void isExplosion()
    {
        
        boom = Instantiate(explosion, new Vector3(transform.position.x,transform.position.y+1.2f,transform.position.z), Quaternion.identity);
        boom.GetComponent<ParticleSystem>().Play();
        Destroy(boom.gameObject,1.5f);
    }
}
