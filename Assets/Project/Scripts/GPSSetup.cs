using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class GPSSetup : MonoBehaviour {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void ActivateGPS() {
            GPSLeaderboard.ActivateGPS();
        }
    }
}