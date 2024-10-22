namespace ToDoList.Domain.DTOs;

using System;
using ToDoList.Domain.Models;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted)
{
    public static ToDoItem ToDomain()
    {
        return new  ToDoItem();
    }
}
