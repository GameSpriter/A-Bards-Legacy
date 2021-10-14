using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableRoom", menuName = "DungeonCreation/ScriptableRoomScriptableObject", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public string Name = "";

    private int width;
    private int height;
    //public TGridObject[,] gridArray;
    public string Key;

    

}