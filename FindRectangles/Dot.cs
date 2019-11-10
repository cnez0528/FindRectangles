using System;
using System.Collections.Generic;
using System.Text;

namespace FindRectangles
{
    public class Dot
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public override bool Equals(object value)
        {
            Dot dot = value as Dot;
            return xCoord == dot.xCoord && yCoord == dot.yCoord;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, xCoord) ? xCoord.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, yCoord) ? yCoord.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
