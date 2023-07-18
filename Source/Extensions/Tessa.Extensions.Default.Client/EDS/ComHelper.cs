using System;
using System.Globalization;
using System.Reflection;

namespace Tessa.Extensions.Default.Client.EDS
{
    public static class ComHelper
    {
        public static object GetComObj(string progId) =>
            Activator.CreateInstance(Type.GetTypeFromProgID(progId) ?? throw new InvalidOperationException("COM type not found by ProgID: " + progId));

        public static void Set(this object obj, string propName, params object[] propValues) =>
            obj.GetType().InvokeMember(propName, BindingFlags.SetProperty, null, obj, propValues, CultureInfo.InvariantCulture);

        public static object Get(this object obj, string propName, params object[] propValues) =>
            obj.GetType().InvokeMember(propName, BindingFlags.GetProperty, null, obj, propValues, CultureInfo.InvariantCulture);

        public static object Invoke(this object obj, string methodName, params object[] propValues) =>
            obj.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, propValues, CultureInfo.InvariantCulture);
    }
}