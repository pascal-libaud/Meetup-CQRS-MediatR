﻿using _2_Blog_CQRS.Common;

namespace _2_Blog_CQRS.Commands.Users;

public record CreateUser(string Name) : ICommand<int>;

public record RenameUser(int Id, string Name) : ICommand;

public record DeleteUser(int Id): ICommand;