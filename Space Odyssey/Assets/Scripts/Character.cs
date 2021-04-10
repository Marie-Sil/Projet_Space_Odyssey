using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

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
        SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
        currSprite.sprite = character.GetComponent<Character>().characterImage;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
