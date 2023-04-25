namespace _2_Blog_CQRS;

public record PostDTO(int Id, string Title, string Author);

public record PostDetailDTO(string Title, string Author, string Content, CommentDTO[] Comments);

public record CommentDTO(string Content, string Author);