using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public Permission[] Permissions { get; set; }

    public AuthorizeAttribute() {}

    public AuthorizeAttribute(params Permission[] permissions)
    {
        Permissions = permissions;
    }
}