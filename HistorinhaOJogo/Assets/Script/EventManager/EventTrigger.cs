using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [field:SerializeField] protected TriggerType Trigger { get; private set; }
    [field:SerializeField] protected int TriggerIndex { get; private set; }
    [field:SerializeField] protected Event Event { get; private set; }
    [field:SerializeField] public Dialogue[] Dialogs { get; private set; }
    private Collider2D collider2d;
    protected enum TriggerType {ON_START, ON_END, ON_INDEX}

    private void Start() {
        if (DialogueManager.Instance == null)
            Debug.LogError("EventTrigger: DialogueManager couldn't be found.");
        
        collider2d = gameObject.GetComponent<Collider2D>();
        if (TriggerIndex >= Dialogs.Length)
            Debug.LogError("EventTrigger: TriggerIndex cannot be greater than the quantity of Dialogs");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Constants.PLAYER_TAG))
            return;
        
        StartEvent();
    }

    public virtual void StartEvent()
    {
        if (DialogueManager.Instance.IsPlaying)
            DialogueManager.Instance.RequestEnd();
        
        DialogueManager.OnDialogueStart += DialogueStarted;
        DialogueManager.OnDialogueEnd += DialogueEnded;
        DialogueManager.OnDialogueChanged += DialogueChanged;
        
        collider2d.enabled = false;
        DialogueManager.Play(Dialogs);
    }

    public virtual void DialogueChanged(int index, Dialogue dialogue)
    {
        if (Trigger == TriggerType.ON_INDEX && TriggerIndex == index)
            TriggerEvent();
    }

    public virtual void DialogueEnded()
    {
        if (Trigger == TriggerType.ON_END)
            TriggerEvent();
        DialogueManager.OnDialogueStart -= DialogueStarted;
        DialogueManager.OnDialogueEnd -= DialogueEnded;
        DialogueManager.OnDialogueChanged -= DialogueChanged;
    }

    public virtual void DialogueStarted()
    {
        if (Trigger == TriggerType.ON_START)
            TriggerEvent();
    }

    public virtual void TriggerEvent()
    {
        string message = $"{Event.GetType().Name} triggered at {Trigger}.";
        if (Trigger == TriggerType.ON_INDEX)
            message += $" Index {TriggerIndex}";
        Debug.Log(message);
        Event.TriggerEvent();
    }
}
