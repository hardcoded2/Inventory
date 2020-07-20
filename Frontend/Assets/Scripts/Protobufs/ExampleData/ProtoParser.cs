using System;
using System.Collections.Generic;
using System.Reflection;
using Google.Protobuf;

namespace ExampleData
{
    public class ProtoParser 
    {
        /*
         //could auto-generate this list
        public IEnumerable<Type> SupportedTypes()
        {
            return new[] {typeof(TileData),};
        }
        */
        public bool IsSupportedType<T>()
        {
            var type = typeof(T);
            
            //TODO: cache results to prevent reflection at runtime
            return HasDefaultConstructor(type) && Implements(type,typeof(IMessage));
        }

        private bool Implements(Type t, Type impl)
        {
            foreach (var @interface in t.GetInterfaces())
            {
                if (@interface == impl) 
                    return true;
            }

            return false;
        }

        private bool HasDefaultConstructor(Type type)
        {
            //manually test for implementing new() ie typeof(T).
            //typeof(T).GetConstructors() -- make sure one is empty
            //Activator.CreateInstance<T> ??
            /*
            foreach (var constructorInfo in typeof(T).GetConstructors())
            {
                var parameters = constructorInfo.GetParameters();
                bool hasEmptyConstructor = parameters.Length == 0;
                
                foreach (var parameterInfo in parameters)
                {
                    //... test for optional params, etc
                }
                if (!hasEmptyConstructor) return false;
            }
            */

            var defaultConstructor = type.GetConstructor(Type.EmptyTypes); //private constructors not allowed
            return defaultConstructor != null;
        }

        
        //TODO: stream, async, etc
        //not sure about interface since IMessage and new would creep up as requirements
        //could use cached reflection for testing some of this, but that also seems odd
        //can loop through all supported types and early out if not supported, a runtime feature only
        public T Parse<T>(byte[] bytes) where T : IMessage<T>, new()
        {
            IMessage tmp = new T();
            return new MessageParser<T>(()=>new T()).ParseFrom(bytes);
        }


        public string ToJson<T>(T message) where T : IMessage
        {
            return JsonFormatter.ToDiagnosticString(message);
        }
    }
/*
    public interface IParser
    {
        IEnumerable<Type> SupportedTypes();
        T FromBytes<T>(byte[] bytes);
    }
*/
}
