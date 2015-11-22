using System;

namespace Lanem.Serializers
{
    public interface IExceptionSerializer
    {
        string Serialize(Exception exception);
    }
}