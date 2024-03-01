using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Mechanics2D
{
    /// <summary>
    /// Classes that implement this interface should have an serialized instance of DataSettings to register through.
    /// </summary>
    public interface IDataPersister
    {
        DataSettings GetDataSettings();

        void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType);

        Data SaveData();

        void LoadData(Data data);
    }

    [Serializable]
    public class DataSettings
    {
        public enum PersistenceType
        {
            DoNotPersist,
            ReadOnly,
            WriteOnly,
            ReadWrite,
        }

        public string dataTag = System.Guid.NewGuid().ToString();
        public PersistenceType persistenceType = PersistenceType.ReadWrite;

        public string dataName = "default";

        public override string ToString()
        {
            return dataTag + " " + persistenceType.ToString();
        }
    }

    [Serializable]
    public class Data
    {
        public virtual void Print()
        {
            Debug.Log("*** Data ***");
        }
    }

    [Serializable]
    public class Data<T0> : Data //, ISerializable
    {
        public T0 value;

        public Data(T0 value0)
        {
            this.value = value0;
        }

        public override void Print()
        {
            base.Print();
            Debug.Log($"{typeof(T0).FullName} {this.value}");
        }
    
        // Конструктор для десериализации
        /*
        protected Data(SerializationInfo info, StreamingContext context)
        {
            value = (T0)info.GetValue("Value0", typeof(T0));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value0", value);
        }
        */
    }

    [Serializable]
    public class Data<T0, T1> : Data //, ISerializable
    {
        public T0 value0;
        public T1 value1;
        public Data(T0 value0, T1 value1)
        {
            this.value0 = value0;
            this.value1 = value1;
        }

        public override void Print()
        {
            base.Print();
            Debug.Log($"{typeof(T0).FullName} {this.value0}");
            Debug.Log($"{typeof(T1).FullName} {this.value1}");
        }
    
        // Конструктор для десериализации
        /*
        protected Data(SerializationInfo info, StreamingContext context)
        {
            value0 = (T0)info.GetValue("Value0", typeof(T0));
            value1 = (T1)info.GetValue("Value1", typeof(T1));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value0", value0);
            info.AddValue("Value1", value1);
        }
        */
    }
    
    [Serializable]
    public class Data<T0, T1, T2> : Data //, ISerializable
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;

        public Data(T0 value0, T1 value1, T2 value2)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
        }

        public override void Print()
        {
            base.Print();
            Debug.Log($"{typeof(T0).FullName} {this.value0}");
            Debug.Log($"{typeof(T1).FullName} {this.value1}");
            Debug.Log($"{typeof(T2).FullName} {this.value2}");
        }
        
        // Конструктор для десериализации
        /*
        protected Data(SerializationInfo info, StreamingContext context)
        {
            value0 = (T0)info.GetValue("Value0", typeof(T0));
            value1 = (T1)info.GetValue("Value1", typeof(T1));
            value2 = (T2)info.GetValue("Value2", typeof(T2));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value0", value0);
            info.AddValue("Value1", value1);
            info.AddValue("Value2", value2);
        }
        */
    }

    [Serializable]
    public class Data<T0, T1, T2, T3> : Data //, ISerializable
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;
        public T3 value3;

        public Data(T0 value0, T1 value1, T2 value2, T3 value3)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public override void Print()
        {
            base.Print();
            Debug.Log($"{typeof(T0).FullName} {this.value0}");
            Debug.Log($"{typeof(T1).FullName} {this.value1}");
            Debug.Log($"{typeof(T2).FullName} {this.value2}");
            Debug.Log($"{typeof(T3).FullName} {this.value3}");
        }
   
        // Конструктор для десериализации
        /*
        protected Data(SerializationInfo info, StreamingContext context)
        {
            value0 = (T0)info.GetValue("Value0", typeof(T0));
            value1 = (T1)info.GetValue("Value1", typeof(T1));
            value2 = (T2)info.GetValue("Value2", typeof(T2));
            value3 = (T3)info.GetValue("Value3", typeof(T3));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value0", value0);
            info.AddValue("Value1", value1);
            info.AddValue("Value2", value2);
            info.AddValue("Value3", value3);
        }
        */
    }

    [Serializable]
    public class Data<T0, T1, T2, T3, T4> : Data //, ISerializable
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;
        public T3 value3;
        public T4 value4;

        public Data(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
        }

        public override void Print()
        {
            base.Print();
            Debug.Log($"{typeof(T0).FullName} {this.value0}");
            Debug.Log($"{typeof(T1).FullName} {this.value1}");
            Debug.Log($"{typeof(T2).FullName} {this.value2}");
            Debug.Log($"{typeof(T3).FullName} {this.value3}");
            Debug.Log($"{typeof(T4).FullName} {this.value4}");
        }
   
        // Конструктор для десериализации
        /*
        protected Data(SerializationInfo info, StreamingContext context)
        {
            value0 = (T0)info.GetValue("Value0", typeof(T0));
            value1 = (T1)info.GetValue("Value1", typeof(T1));
            value2 = (T2)info.GetValue("Value2", typeof(T2));
            value3 = (T3)info.GetValue("Value3", typeof(T3));
            value4 = (T4)info.GetValue("Value4", typeof(T4));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value0", value0);
            info.AddValue("Value1", value1);
            info.AddValue("Value2", value2);
            info.AddValue("Value3", value3);
            info.AddValue("Value4", value4);
        }
        */
    }

}