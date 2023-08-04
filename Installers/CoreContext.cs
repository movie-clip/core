using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zenject;

namespace Core.Installers
{
    public class CoreContext : SceneContext
    {
        protected override void RunInternal()
        {
            var installers = GetEnumerableOfType<BaseInstaller>();
            foreach (BaseInstaller baseInstaller in installers)
            {
                AddNormalInstaller(baseInstaller);
            }
            
            base.RunInternal();
        }

        
        private IEnumerable<T> GetEnumerableOfType<T>() where T : BaseInstaller
        {
            List<T> objects = new List<T>();
            foreach (Type type in 
                Assembly.GetAssembly(typeof(T)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type));
            }
            
            return objects;
        }
    }
}