using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue System/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject {
        [field:SerializeField] public string Name { get; private set; } = string.Empty;
        [field:SerializeField][field:Multiline(8)] public string Text { get; private set; } = string.Empty;
        [field:SerializeField] public float SpeechSpeed { get; private set; } = .02f;
        [field:SerializeField] public float AutoAvanceTime { get; private set; } = -1f;
        [field:SerializeField] public DialoguePanelSide PanelSide { get; private set; } = DialoguePanelSide.LEFT;
        public bool IsAutoAdvance => AutoAvanceTime > 0f;

        public enum DialoguePanelSide { RIGHT, LEFT }
    }
}