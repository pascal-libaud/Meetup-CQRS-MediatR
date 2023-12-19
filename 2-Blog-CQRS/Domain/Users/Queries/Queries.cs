using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Domain.Users.Queries;

public record GetAllUsers : IQuery<UserDTO[]>;
