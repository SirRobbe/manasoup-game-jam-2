using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static List<Turret> s_Turrets = new List<Turret>();
    public static List<Nightmare> s_Nightmares = new List<Nightmare>();
    public static List<CardType> s_SelectedCardTypes = new List<CardType>();
}