using System;

namespace HC.Application.DTOs;

public class GetStoryFiles
{
    public GetStoryFiles(int id, Guid fileId, DateTime dateAdded, string name, byte[] bytes)
    {
        Id = id;
        FileId = fileId;
        DateAdded = dateAdded;
        Name = name;
        Bytes = bytes;
    }

    public int Id { get; }
    public Guid FileId { get; }
    public DateTime DateAdded { get; }
    public string Name { get; }
    public byte[] Bytes { get; }
}