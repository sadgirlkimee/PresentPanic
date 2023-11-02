using UnityEngine;

public class GameCharManager : MonoBehaviour
{
    public static GameCharManager instance;
    public SelCharacter[] selcharacters;
    public SelCharacter currentSelCharacter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (selcharacters.Length > 0)
        {
            currentSelCharacter = selcharacters[0];
        }
    }

    public void SetCharacter(SelCharacter selcharacter)
    {
        currentSelCharacter = selcharacter;
    }
}


