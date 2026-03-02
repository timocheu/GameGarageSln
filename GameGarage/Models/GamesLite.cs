using System;
using System.Collections.Generic;

namespace GameGarage.Models;

public partial class GamesLite
{
    public long? AppId { get; set; }

    public string? Name { get; set; }

    public string? ReleaseDate { get; set; }

    public string? EstimatedOwners { get; set; }

    public long? PeakCcu { get; set; }

    public long? RequiredAge { get; set; }

    public double? Price { get; set; }

    public long? DiscountDlcCount { get; set; }

    public string? AboutTheGame { get; set; }

    public string? SupportedLanguages { get; set; }

    public string? FullAudioLanguages { get; set; }

    public string? Reviews { get; set; }

    public string? HeaderImage { get; set; }

    public string? Website { get; set; }

    public string? SupportUrl { get; set; }

    public string? SupportEmail { get; set; }

    public string? Windows { get; set; }

    public string? Mac { get; set; }

    public string? Linux { get; set; }

    public long? MetacriticScore { get; set; }

    public string? MetacriticUrl { get; set; }

    public long? UserScore { get; set; }

    public long? Positive { get; set; }

    public long? Negative { get; set; }

    public string? ScoreRank { get; set; }

    public long? Achievements { get; set; }

    public long? Recommendations { get; set; }

    public string? Notes { get; set; }

    public long? AveragePlaytimeForever { get; set; }

    public long? AveragePlaytimeTwoWeeks { get; set; }

    public long? MedianPlaytimeForever { get; set; }

    public long? MedianPlaytimeTwoWeeks { get; set; }

    public string? Developers { get; set; }

    public string? Publishers { get; set; }

    public string? Categories { get; set; }

    public string? Genres { get; set; }

    public string? Tags { get; set; }

    public string? Screenshots { get; set; }
}
