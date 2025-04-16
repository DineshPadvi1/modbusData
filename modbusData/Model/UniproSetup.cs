using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("dataset")]
public class UniproModbus
{
    [Key]
    [Column("index")]
    public int Index { get; set; }

    [Column("date")]
    public string Date { get; set; }

    [Column("length")]
    public int? Length { get; set; }

    [Column("data")]
    public string Data { get; set; }

    [Column("Status")]
    public int Status { get; set; }  // Assuming you have this column in table, else remove
}
