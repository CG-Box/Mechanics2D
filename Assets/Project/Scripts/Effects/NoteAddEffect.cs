using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Note Add")]
public class NoteAddEffect : Effect
{
    [SerializeField]private NoteEventChannelSO addNoteRequest = default;
    [SerializeField]private NoteType type;
    [SerializeField]private string text;

    public override void Apply()
    {
        base.Apply();

        //add note to chronicles
        addNoteRequest.RaiseEvent(new Note(type, text));
    }
}

