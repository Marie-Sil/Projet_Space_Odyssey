using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Sprite characterImage;
    public string characterName;
    //public DialogueParser2 characterDialogue;
    public Text nameBox;

    // Start is called before the first frame update
    void Start()
    {
        nameBox.text = characterName;

        GameObject character = GameObject.Find(characterName);
        SpriteRenderer currSprite = GetComponent<SpriteRenderer>();
        currSprite.sprite = characterImage;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
