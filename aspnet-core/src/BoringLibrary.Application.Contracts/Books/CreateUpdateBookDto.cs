using System.ComponentModel.DataAnnotations;

namespace BoringLibrary.Books;

public class CreateUpdateBookDto
{
    [Required] [StringLength(128)] public string Name { get; set; }

    [Required] public int Credit { get; set; }

    [Required] public int Quantity { get; set; }
}