﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TopDownShooter
{
    public class MenuOption
    {
        public string Text { get; }
        public Action Action { get; }

        public MenuOption(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }

}
