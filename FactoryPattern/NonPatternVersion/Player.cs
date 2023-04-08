﻿namespace Engine.FactoryPattern.NonPatternVersion
{
    public class Player
    {
        public int HitPoints { get; private set; }
        public int ExperiencePoints { get; private set; }
        public int Gold { get; private set; }

        public Player(int hitPoints, int experiencePoints, int gold)
        {
            HitPoints = hitPoints;
            ExperiencePoints = experiencePoints;
            Gold = gold;
        }
    }
}