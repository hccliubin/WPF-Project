﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageView
{
    public class LoactionArgs : EventArgs
    {
         public readonly double Left;
        public readonly double Top;
        public LoactionArgs(double left, double top)
        {
            Left = left;
            Top = top;
        }

    }
}
