using UnityEngine;
[CreateAssetMenu(fileName = "SelectObject", menuName = "ScriptableObjects/SelectData", order = 1)]

public class SelectData : ScriptableObject
{
    public string NameOfTheButton;
    public string AudioToPlay;
    public string InfoTextToPlay; 
    public UpgradeDataContainer UpgradeContainer;
}
