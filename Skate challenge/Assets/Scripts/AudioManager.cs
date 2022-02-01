using UnityEngine;

/*
 * Аудио эфекты сбора монет, пуска ракеты, взрыва ракеты, сбора ракет, меню, удара о препятствия, меню и фоновой музыки
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public GameManager GM;
    public AudioSource BGAudio, EffectsAudio;
    public AudioClip CoinSound;
    public AudioClip RocketStartSound;
    public AudioClip RocketBoomSound;
    public AudioClip MenuSound;
    public AudioClip HitSound;
    public AudioClip RocketUpSound;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RefreshSoundState()
    {
        if(GM.IsSound)
            BGAudio.UnPause();
        else
            BGAudio.Pause();
    }

    public void PlayCoinEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(CoinSound);
    }
    
    public void PlayRocketStartEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(RocketStartSound);
    }
    public void PlayRocketBoomEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(RocketBoomSound);
    }
    public void MenuEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(MenuSound);
    }
    public void HitEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(HitSound);
    }
    public void RocketEffect()
    {
        if(GM.IsSound)
            EffectsAudio.PlayOneShot(RocketUpSound);
    }
}
