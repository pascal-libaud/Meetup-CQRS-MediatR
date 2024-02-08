using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Domain.Users.Queries;

public record GetAllUsers : IQuery<UserDTO[]>;

public record GetActiveUsers : IQuery<UserDTO[]>;

public record GetDetailedUser(int Id) : IQuery<UserDetailDTO>;