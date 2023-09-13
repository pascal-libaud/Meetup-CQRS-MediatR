using System.ComponentModel.DataAnnotations;

namespace _1_Blog_CQRS_Less.Models;

public record PostDTO(int Id, string Title, string Author);

public record PostDetailDTO(string Title, string Author, string Content, CommentDTO[] Comments);

public record CommentDTO(string Content, string Author);

public class CreatePost
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(2000)]
    public required string Content { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Author { get; set; }
}

public record CreateUser(string Name);

public record RenameUser(int Id, string Name);

public record DeleteUser(int Id);