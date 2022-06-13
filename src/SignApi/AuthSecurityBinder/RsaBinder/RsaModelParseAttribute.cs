using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SignApi.AuthSecurityBinder.RsaBinder
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class RsaModelParseAttribute : Attribute, IBinderTypeProviderMetadata, IBindingSourceMetadata, IModelNameProvider
    {
        private readonly ModelBinderAttribute modelBinderAttribute = new ModelBinderAttribute() { BinderType = typeof(EncryptBodyModelBinder) };

        public BindingSource BindingSource => modelBinderAttribute.BindingSource;

        public string Name => modelBinderAttribute.Name;

        public Type BinderType => modelBinderAttribute.BinderType;
    }
}
