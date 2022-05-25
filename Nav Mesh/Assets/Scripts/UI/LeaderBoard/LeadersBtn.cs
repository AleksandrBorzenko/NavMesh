using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Button in canvas which is responsible for opening and closing a leaderboard
/// </summary>
public class LeadersBtn : MonoBehaviour,ILeadersBtn
{
    [SerializeField] private GameObject leaderboard;
    /// <summary>
    /// If leaderboard is open
    /// </summary>
    public bool isOpen { get; set; }
    /// <summary>
    /// Method for opening and closing a leaderboard
    /// </summary>
    public void OpenCloseLeaderboard()
    {
        if (isOpen)
        {
            leaderboard.SetActive(false);
            isOpen = false;
        }
        else
        {
            leaderboard.SetActive(true);
            isOpen = true;
        }
    }

}
