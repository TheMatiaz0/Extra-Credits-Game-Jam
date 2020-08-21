﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
namespace Cyberultimate
{
    public static class MathHelper
    {
        /// <summary>
        /// Converts the value from the basic range to a value from the needed range.
        /// </summary>
        /// <param name="basic">Basic range.</param>
        /// <param name="needed">Needed range.</param>
        /// <param name="value">Value from the basic range.</param>
        /// <returns></returns>
        public static double ReCalculateRange((double min, double max) basic, (double min, double max) needed, double value)
        {
            return (value - basic.min) / (basic.max - basic.min) * (needed.max - needed.min) + needed.min;
        }
        /// <summary>
        /// Converts the value from basic range to a value from needed range.
        /// </summary>
        /// <param name="basic">Basic range.</param>
        /// <param name="needed">Needed range.</param>
        /// <param name="value">Value from basic range.</param>
        /// <returns></returns>
        public static float ReCalculateRange((float min, float max) basic, (float min, float max) needed, double value)
        {
            return ReCalculateRange(basic, needed, value);
        }

        public static T Clamp<T>(T value, T min, T max)
            where T:IComparable<T>
        {
           
            int maxCompare = value.CompareTo(max);
            if (maxCompare == 1 || maxCompare == 0)
                return max;
            int minComparer = value.CompareTo(min);
            if (minComparer == 1)
                return value;
            else return min;

        }
        
       
    }
}
