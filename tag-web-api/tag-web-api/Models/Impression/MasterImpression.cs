using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAGWEBAPI.Models;

[Table("master_impressions", Schema = "public")]
public class MasterImpression
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("emoji")]
    public string Emoji { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("label")]
    public string Label { get; set; } = null!;

    [Column("display_order")]
    public int DisplayOrder { get; set; }
}