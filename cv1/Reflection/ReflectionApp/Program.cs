

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;




namespace ReflectionApp
{
    public class Program
    {
        public static void Main(string[] args) {
            Run("/Customer/Add?Name=Pepa&Age=30&IsActive=true");
            Run("/Customer/Add?Name=Pepa&Age=30&IsActive=true");
        }


        private static void Run(string url)
        {
            //string url = "/Customer/List?limit=2";
            string[] mainParts = url.Split('?');
            string[] pathParts = mainParts[0].Split('/');
            string[] queryParts = mainParts[1].Split('&');

            Dictionary<string, string> queryDict = queryParts
                .Select(x => x.Split('='))
                .ToDictionary(x =>x[0], y =>y[1], StringComparer.OrdinalIgnoreCase);

            string controller = pathParts[1];
            string action = pathParts[2];

            string path = "../../../../MyLib/bin/Debug/net5.0/MyLib.dll";
            path = Path.GetFullPath(path);

            Assembly assembly = Assembly.LoadFile(path);

            //foreach (Type type in assembly.GetTypes()) {
               // Console.WriteLine(type.FullName);
            //}

            Type controllerType = assembly.GetType($"MyLib.Controllers.{controller}Controller");

            //object instance1 = assembly.CreateInstance("MyLib.Controllers.CustomerController");
            object instance = Activator.CreateInstance(controllerType);
            //object instance3 = controllerType.Assembly.CreateInstance(controllerType.Name);

            
            MethodInfo myMethod = controllerType.GetMethod(action);

            List<object> arguments = new List<object>();
            foreach (ParameterInfo par in myMethod.GetParameters()) {

                if (par.ParameterType.IsClass && par.ParameterType != typeof(string)) {

                    object obj = Activator.CreateInstance(par.ParameterType);

                    foreach (PropertyInfo prop in par.ParameterType.GetProperties()) {

                        if (!queryDict.ContainsKey(prop.Name)) { continue; }

                        string val2 = queryDict[prop.Name];
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(obj, int.Parse(val2));
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(obj, val2);
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            prop.SetValue(obj, bool.Parse(val2));
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        prop.SetValue(obj, val2);
                    }

                    //foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    //{}

                    arguments.Add(obj);
                    continue;
                }

                string val = queryDict[par.Name];

                if (par.ParameterType == typeof(int))
                {
                    arguments.Add(int.Parse(val));
                }
                else if (par.ParameterType == typeof(string))
                {
                    arguments.Add(val);
                }
                else if (par.ParameterType == typeof(bool))
                {
                    arguments.Add(bool.Parse(val));
                }
                else {
                    throw new NotImplementedException();
                }

            }

            object result = myMethod.Invoke(instance, arguments.ToArray());
            Console.WriteLine(result);


            
            



        }

    }
}