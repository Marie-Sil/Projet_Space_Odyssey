using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

public class DialogueParser2 : MonoBehaviour
{
    [SerializeField] public DialogueContainer dialogue;
    [SerializeField] public Text dialogueText;
    [SerializeField] public Button choicePrefab;
    [SerializeField] public Transform buttonContainer;

    public void Start()
    {
        var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
        ProceedToNarrative(narrativeData.TargetNodeGUID);
    }

    private void ProceedToNarrative(string narrativeDataGUID)
        {
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            dialogueText.text = ProcessProperties(text);
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            int j = 0;
            foreach (var choice in choices)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.transform.localPosition = new Vector3(-200 + (j * 400), -110);
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
    }

