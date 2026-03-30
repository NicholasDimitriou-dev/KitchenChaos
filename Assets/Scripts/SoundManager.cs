using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
   public static SoundManager Instance { get; private set; }
   [SerializeField] private AudioClipCO audioClipCo;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSucess;
      DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
      CuttingCounter.OnAnyCut += CC_OnAnyCut;
      Player.Instance.OnPickup += Player_OnPickup;
      BaseCounter.OnDrop += BaseCounterOnOnDrop;
      TrashCounter.trashing += TrashCounterOntrashing;
   }
   private void TrashCounterOntrashing(object sender, EventArgs e)
   {
      TrashCounter tCounter = sender as TrashCounter;
      PlaySound(audioClipCo.trash, tCounter.transform.position);  
   }

   private void BaseCounterOnOnDrop(object sender, EventArgs e)
   {
      BaseCounter bCounter = sender as BaseCounter;
      PlaySound(audioClipCo.drop, bCounter.transform.position);
   }

   private void Player_OnPickup(object sender, EventArgs e)
   {
      PlaySound(audioClipCo.pickup, Player.Instance.transform.position);
   }

   private void CC_OnAnyCut(object sender, EventArgs e)
   {
      CuttingCounter cuttingCounter = sender as CuttingCounter;
      PlaySound(audioClipCo.chop, cuttingCounter.transform.position);
   }

   private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
   {
      Delivery delivery = Delivery.Instance;
      PlaySound(audioClipCo.deliveryFail, delivery.transform.position);
   }

   private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
   {
      Delivery delivery = Delivery.Instance;
      PlaySound(audioClipCo.deliverySuccess,  delivery.transform.position);
   }
   private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f){
      PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
   }
   private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f){
      AudioSource.PlayClipAtPoint(audioClip, position, volume);
   }

   public void PlayFootstepsSound(Vector3 position, float volume = 1f)
   {
      PlaySound(audioClipCo.footstep, position, volume);
   }
}
