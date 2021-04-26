using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;
using System.IO;
using UnityEditor;

public class DialogueParser2 : MonoBehaviour
{
    [SerializeField] public DialogueContainer dialogue;
    [SerializeField] public Text dialogueText;
    [SerializeField] public Button choicePrefab;
    [SerializeField] public Transform buttonContainer;
    [SerializeField] public string goodAnswer;
    [SerializeField] public string nameDoor;
    private string firstText;

    public void Start()
    {
        firstText = "Tu te trouves actuellement sur le quai de d�barquement de cette station, bien d�termin� � retrouver ton ami ! Clic sur la porte au fond pour acc�der au Hub.";
        var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
        ProceedToNarrative(narrativeData.TargetNodeGUID);
    }

    private void ProceedToNarrative(string narrativeDataGUID)
        {
            // R�cur�ration du texte et des choix de r�ponse
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            
            // Affichage du texte
            dialogueText.text = ProcessProperties(text);

            // V�rification si le texte est le texte permettant de d�bloquer la suite
            if (text == goodAnswer)
            {
                ChangeState(text);
            }

            // Destruction des anciens boutons
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            // Cr�ation des nouveaux boutons
            int j = 0;
            foreach (var choice in choices)
            {
                // Instancie le bouton
                var button = Instantiate(choicePrefab, buttonContainer);
                // Affiche le texte
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                // Positionne le bouton
                button.transform.localPosition = new Vector3(-200 + (j * 400), -110);
                // Instancie la fonction appel�e si on click sur le bouton
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
                j++;
            }
        }

        private string ProcessProperties(string text)
        {
            foreach (var exposedProperty in dialogue.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }

    private void ChangeState(string text)
    {
        List<Unblock.FileLine> doors = new List<Unblock.FileLine>();

        // R�cup�ration des donn�es
        doors = Unblock.LoadData();

        // R��criture des donn�es
        StreamWriter writer = new StreamWriter("Assets/Data/Doors.txt");
        string line;

        using (writer)
        {
            foreach (Unblock.FileLine door in doors)
            {
                if (door.nameDoor == nameDoor || (door.state == "unlock" && text != firstText))
                {
                    line = door.nameDoor + ";unlock";
                    writer.WriteLine(line);
                }
                else
                {
                    line = door.nameDoor + ";lock";
                    writer.WriteLine(line);
                }
            }
            writer.Close();
        }
    }

}

