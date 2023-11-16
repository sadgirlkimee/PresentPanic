using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharaterSelectionUI : MonoBehaviour {
    public GameObject optionPrefab;

    public Transform prevCharacter;
    public Transform selectedCharacter;

    private void Start() {
        foreach (SelCharacter c in GameCharManager.instance.selcharacters) {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();

            button.onClick.AddListener(() => {
                GameCharManager.instance.SetCharacter(c);
                if (selectedCharacter != null) {
                    prevCharacter = selectedCharacter;
                }

                selectedCharacter = option.transform;
            });

            TextMeshProUGUI text = option.GetComponentInChildren<TextMeshProUGUI>();
                text.text = c.name;

            Image image = option.GetComponentInChildren<Image>();
                image.sprite = c.icon;
        }
    }

    private void Update() {
        if (selectedCharacter != null) {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        if (prevCharacter != null) {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}