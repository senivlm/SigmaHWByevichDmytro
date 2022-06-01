﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Pair<T>
    {
        private T _element;
        private int _freq;
        public T Element { get => _element; set => _element = value; }
        public int Freq { get => _freq; set => _freq = value; }

        public Pair() : this(default, default) { }
        public Pair(T element, int freq)
        {
            Element = element;
            Freq = freq;
        }

        public override string ToString()
        {
            return $"{Element} - {Freq}";
        }
    }
}
