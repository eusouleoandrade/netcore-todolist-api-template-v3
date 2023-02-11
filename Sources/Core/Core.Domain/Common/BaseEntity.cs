using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Core.Domain.Common
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity<TId>
        where TId : struct
    {
        [Key]
        public TId Id { get; protected set; }
    }
}