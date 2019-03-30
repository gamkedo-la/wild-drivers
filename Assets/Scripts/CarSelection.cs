using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarSelection
{
    public static string currentVehicle { get; set; } = "RaceCar";
    public static string currentMode { get; set; } = "Multiplayer";
    public static AudioListener audioListener { get; set; }
}
