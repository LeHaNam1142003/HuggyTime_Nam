using System.Collections;
using System.Collections.Generic;
using nameTag;
using Spine.Unity;
using UnityEngine;

namespace Component
{
    public static class StateAnimPlayer
    {
        public const string Attack = "Attack";
        public const string Attack2 = "Attack2";
        public const string Cry = "Cry";
        public const string Cry2 = "Cry2";
        public const string Die = "Die";
        public const string Diefire = "DieFire";
        public const string Home = "Home";
        public const string Ice = "Ice";
        public const string Idle = "Idle";
        public const string Open = "Open";
        public const string Pickup = "PickUp";
        public const string Run = "Run";
        public const string See = "See";
        public const string Walk = "Walk";
        public const string Walk2 = "Walk2";
        public const string Win = "Win";
        public const string Yawn = "Yawn";
        public const string Speed = "Speed";
        public const string Find = "Find";
    }

    public static class ConstAnimationPlayer
    {
        public const float Attack = 1.6f;
        public const float Attack2 = 1.333f;
        public const float Cry = 0.233f;
        public const float Cry2 = 1.000f;
        public const float Die = 0.800f;
        public const float DieFire = 0.867f;
        public const float Home = 13.533f;
        public const float Ice = 0.833f;
        public const float Idle = 1.33f;
        public const float Open = 1.633f;
        public const float Pickup = 0.833f;
        public const float Run = 0.8f;
        public const float See = 2.3f;
        public const float Walk = 1.067f;
        public const float Walk2 = 1.33f;
        public const float Win = 1.33f;
        public const float Yawn = 1.467f;
    }
    public static class StateAnimEnemy
    {
        public const string Idle = "Idle";
        public const string Afraid = "Afraid";
        public const string Die = "Die";
        public const string DieIce = "DieIce";
        public const string DieFire = "DieFire";
    }

    public static class PlayerEvent
    {
        public const string EndAttack = "EndAttack";
        public const string OnBullet = "OnBullet";
    }
}

