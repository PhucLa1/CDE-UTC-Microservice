using System.Reflection;
using System.Reflection.Emit;

namespace BuildingBlocks.Extensions
{
    public static class ObjectExtensions
    {
        public static Type Omit<T>(params string[] propertiesToExclude)
        {
            var type = typeof(T);

            // Tạo assembly động
            AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            // Tạo module để chứa class động
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

            // Tạo class động với tên của class gốc
            TypeBuilder typeBuilder = moduleBuilder.DefineType(type.Name + "_Omitted", TypeAttributes.Public);

            // Lấy tất cả các thuộc tính của class gốc
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Nếu thuộc tính không có trong danh sách propertiesToExclude, ta sẽ thêm vào typeBuilder
                if (!propertiesToExclude.Contains(property.Name))
                {
                    var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);
                    var fieldBuilder = typeBuilder.DefineField("_" + property.Name, property.PropertyType, FieldAttributes.Private);

                    // Tạo getter và setter cho property
                    MethodBuilder getterBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, property.PropertyType, Type.EmptyTypes);
                    ILGenerator getterIl = getterBuilder.GetILGenerator();
                    getterIl.Emit(OpCodes.Ldarg_0);
                    getterIl.Emit(OpCodes.Ldfld, fieldBuilder);
                    getterIl.Emit(OpCodes.Ret);

                    MethodBuilder setterBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { property.PropertyType });
                    ILGenerator setterIl = setterBuilder.GetILGenerator();
                    setterIl.Emit(OpCodes.Ldarg_0);
                    setterIl.Emit(OpCodes.Ldarg_1);
                    setterIl.Emit(OpCodes.Stfld, fieldBuilder);
                    setterIl.Emit(OpCodes.Ret);

                    // Thêm getter và setter vào property
                    propertyBuilder.SetGetMethod(getterBuilder);
                    propertyBuilder.SetSetMethod(setterBuilder);
                }
            }

            // Tạo ra type mới
            return typeBuilder.CreateType();
        }
    }
}
