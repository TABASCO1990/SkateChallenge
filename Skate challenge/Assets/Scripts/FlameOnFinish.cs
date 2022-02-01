using UnityEngine;
/*
 * Вызом анимации по тригеру (фаершоу на финише)
 */ 
public class FlameOnFinish : MonoBehaviour
{
    [SerializeField] private ParticleSystem flame1;
    [SerializeField] private ParticleSystem flame2;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flame1.Play();
            flame2.Play();
        }
    }
}
