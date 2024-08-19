using MyBlog.Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValueOf;

namespace MyBlog.Contracts.Domains;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    [Key]

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", TypeName = "int")]
    public TKey Id { get; set; }
}