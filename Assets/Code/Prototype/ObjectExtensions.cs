using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Asteroids.Prototype
{
    public static partial class ObjectExtensions
    {
        public static T DeepCopy<T>(this T self)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("Type must be iserializable");
            }
            if (ReferenceEquals(self, null))
            {
                return default;
            }
           
            var str = JsonUtility.ToJson(self);

            var ret = JsonUtility.FromJson<T>(str);
            return ret;
            
        }
    }
}