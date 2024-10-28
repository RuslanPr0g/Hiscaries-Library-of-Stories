﻿using System;

namespace HC.API.Requests.Genres;

public sealed class UpdateGenreRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }
}