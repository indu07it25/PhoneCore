﻿using System;

namespace PhoneCore.Framework
{
    /// <summary>
    /// Tuple implementation
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public struct Tuple<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Tuple(T1 item1, T2 item2):this()
        {
            Item1 = item1;
            Item2 = item2;
        }

        //TODO make IEquatable
    } 
}
