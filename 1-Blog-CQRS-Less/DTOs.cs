using System.ComponentModel.DataAnnotations;

namespace _1_Blog_CQRS_Less;

public record CommentDTO(string Content, string Author);

public class PostDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; set; }

    [MaxLength(2000)]
    public string? Content { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Author { get; set; }

    public CommentDTO[]? Comments { get; set; }

    public byte[]? Image { get; set; }
}

public record UserDTO(int Id, string Name);
