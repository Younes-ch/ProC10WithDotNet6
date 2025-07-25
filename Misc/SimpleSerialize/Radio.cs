﻿namespace SimpleSerialize;

public class Radio
{
    public bool HasTweeters;
    public bool HasSubWoofers;
    public List<double> StationPresets;
    public string RadioId = "XF-552RR6";
    public override string ToString()
    {
        var presets = string.Join(",", StationPresets.Select(i => i.ToString()).ToList());
        return $"HasTweeters:{HasTweeters} HasSubWoofers:{HasSubWoofers} Station Presets:{presets}";
    }
}
