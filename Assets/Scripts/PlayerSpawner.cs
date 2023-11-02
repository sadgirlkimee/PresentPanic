using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
   private void Start(){
    Instantiate(GameCharManager.instance.currentSelCharacter.prefab, transform.position,
     Quaternion.identity);
   }
}
