using System.Collections.Generic;

public class FeatureCollection
{
    // Root object: contains a list of features
    public List<Feature> Features { get; set; }
}

public class Feature
{
    // Each feature has properties (place, mag, etc.)
    public Properties Properties { get; set; }
}

public class Properties
{
    // Magnitude can be null, so use nullable double
    public double? Mag { get; set; }

    // Location description
    public string Place { get; set; }
}
