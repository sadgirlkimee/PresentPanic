using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YourNamespace;
public class Player : MonoBehaviour
{

    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("selectedOption"))
        {
            Load();
        }
        else
        {
            selectedOption = 0;
        }


        UpdateCharacter(selectedOption);
    }

      private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.charactersprite;
        
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    
}
