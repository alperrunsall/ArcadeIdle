using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Foods : ScriptableObject
{
    public string foodName;
    public GameObject foodModel;
    public int foodLevel;
    public int foodFee;

}

